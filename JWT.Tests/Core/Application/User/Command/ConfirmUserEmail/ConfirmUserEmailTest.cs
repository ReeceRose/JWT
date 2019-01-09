using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JWT.Application.User.Command.ConfirmUserEmail;
using JWT.Application.User.Model;
using JWT.Application.User.Query.GetUserById;
using JWT.Application.Utilities;
using JWT.Domain.Entities;
using JWT.Domain.Exceptions;
using JWT.Tests.Helpers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace JWT.Tests.Core.Application.User.Command.ConfirmUserEmail
{
    public class ConfirmUserEmailTest
    {
        public Mock<IMediator> Mediator { get; }
        public Mock<MockUserManager> UserManager { get; }
        public ConfirmUserEmailCommandHandler Handler { get; }
        public IMapper Mapper { get; }

        public ConfirmUserEmailTest()
        {
            // Arrange
            Mediator = new Mock<IMediator>();
            UserManager = new Mock<MockUserManager>();
            Mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile())));
            Handler = new ConfirmUserEmailCommandHandler(Mediator.Object, UserManager.Object, Mapper);
        }

        [Theory]
        [InlineData("1234234", "0954368")]
        [InlineData("0954368", "1234234")]
        public void ConfirmUserEmail_SuccessfullyConfirmsEmail(string userId, string token)
        {
            // Arrange
            var requestedUser = new ApplicationUser()
            {
                Id =  userId
            };
            Mediator.Setup(m => m.Send(It.IsAny<GetUserByIdQuery>(), default(CancellationToken))).Returns(Task.FromResult(requestedUser));
            UserManager.Setup(u => u.ConfirmEmailAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).Returns(Task.FromResult(IdentityResult.Success));
            var query = new ConfirmUserEmailCommand(userId, token);
            // Act
            var result = Handler.Handle(query, CancellationToken.None).Result;
            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData("1234234", "0954368")]
        [InlineData("0954368", "1234234")]
        public async Task ConfirmUser_ThrowsInvalidUserException(string userId, string token)
        {
            // Arrange
            Mediator.Setup(m => m.Send(It.IsAny<GetUserByIdQuery>(), default(CancellationToken))).Returns(Task.FromResult((ApplicationUser) null));
            var query = new ConfirmUserEmailCommand(userId, token);
            // Act / Assert
            await Assert.ThrowsAsync<InvalidUserException>(() => Handler.Handle(query, CancellationToken.None));
        }
    }
}
