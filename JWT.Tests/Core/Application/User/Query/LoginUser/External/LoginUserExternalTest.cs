using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JWT.Application.User.Command.CreateUser;
using JWT.Application.User.Query.GenerateLoginToken;
using JWT.Application.User.Query.GetUserByEmail;
using JWT.Application.User.Query.GetUserClaim;
using JWT.Application.User.Query.LoginUser.External;
using JWT.Application.Utilities;
using JWT.Domain.Entities;
using JWT.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace JWT.Tests.Core.Application.User.Query.LoginUser.External
{
    public class LoginUserExternalTest
    {
        public Mock<IMediator> Mediator { get; }
        public IMapper Mapper { get; }
        public LoginUserExternalQueryHandler Handler { get; }

        public LoginUserExternalTest()
        {
            // Arrange
            Mediator = new Mock<IMediator>();
            Mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile())));
            Handler = new LoginUserExternalQueryHandler(Mediator.Object, Mapper);
        }

        [Theory]
        [InlineData("user@test.com", "0987654321", "1234567890")]
        [InlineData("user@domain.com", "1234567890", "0987654321")]
        public async Task LoginUserExternal_ReturnsToken(string email, string accessToken, string jwtToken)
        {
            // Arrange
            var user = new ApplicationUser()
            {
                Email = email
            };
            var claims = new List<Claim>();
            Mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), default(CancellationToken))).ReturnsAsync(user);
            Mediator.Setup(m => m.Send(It.IsAny<GetUserClaimQuery>(), default(CancellationToken))).ReturnsAsync(claims);
            Mediator.Setup(m => m.Send(It.IsAny<GenerateLoginTokenQuery>(), default(CancellationToken))).ReturnsAsync(jwtToken);
            // Act
            var result = await Handler.Handle(new LoginUserExternalQuery(email, accessToken), CancellationToken.None);
            Assert.Equal(jwtToken, result);
        }
        [Theory]
        [InlineData("user@test.com", "0987654321", "1234567890")]
        [InlineData("user@domain.com", "1234567890", "0987654321")]
        public async Task LoginUserExternal_CreatesUserThenReturnsToken(string email, string accessToken, string jwtToken)
        {
            // Arrange
            var claims = new List<Claim>();
            Mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), default(CancellationToken))).ReturnsAsync((ApplicationUser) null);
            Mediator.Setup(m => m.Send(It.IsAny<CreateUserCommand>(), default(CancellationToken))).ReturnsAsync(IdentityResult.Success);
            Mediator.Setup(m => m.Send(It.IsAny<GetUserClaimQuery>(), default(CancellationToken))).ReturnsAsync(claims);
            Mediator.Setup(m => m.Send(It.IsAny<GenerateLoginTokenQuery>(), default(CancellationToken))).ReturnsAsync(jwtToken);
            // Act
            var result = await Handler.Handle(new LoginUserExternalQuery(email, accessToken), CancellationToken.None);
            Assert.Equal(jwtToken, result);
        }

        [Theory]
        [InlineData("user@test.com", "0987654321")]
        [InlineData("user@domain.com", "1234567890")]
        public async Task LoginUserExternal_FailedToCreateNewUser(string email, string accessToken)
        {
            // Arrange
            Mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), default(CancellationToken))).ReturnsAsync((ApplicationUser)null);
            Mediator.Setup(m => m.Send(It.IsAny<CreateUserCommand>(), default(CancellationToken))).ReturnsAsync(IdentityResult.Failed());
            // Act / Assert
            await Assert.ThrowsAsync<InvalidRegisterException>(() => Handler.Handle(new LoginUserExternalQuery(email, accessToken), CancellationToken.None));
        }
    }
}
