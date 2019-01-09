using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JWT.Application.User.Model;
using JWT.Application.User.Query.GenerateLoginToken;
using JWT.Application.User.Query.GetUserByEmail;
using JWT.Application.User.Query.LoginUser;
using JWT.Application.Utilities;
using JWT.Domain.Entities;
using JWT.Domain.Exceptions;
using JWT.Tests.Helpers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace JWT.Tests.Core.Application.User.Query.LoginUser
{
    public class LoginUserTest
    {
        public Mock<IMediator> Mediator { get; }
        public Mock<MockSignInManager> SignInManager { get; }
        public Mock<MockUserManager> UserManager { get; }
        public IMapper Mapper { get; }
        public LoginUserQueryHandler Handler { get; }

        public LoginUserTest()
        {
            // Arrange
            Mediator = new Mock<IMediator>();
            SignInManager = new Mock<MockSignInManager>();
            UserManager = new Mock<MockUserManager>();
            Mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile())));
            Handler = new LoginUserQueryHandler(Mediator.Object, SignInManager.Object, UserManager.Object, Mapper);
        }

        [Theory]
        [InlineData("test@test.ca", "Test123!", "1234567890")]
        [InlineData("user@domain.com", "Password!1f4", "0987654321")]
        public void LoginUser_ReturnsValidToken(string email, string password, string token)
        {
            // Arrange
            var requestedUser = new ApplicationUser()
            {
                Email = email
            };
            Mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), default(CancellationToken))).Returns(Task.FromResult(requestedUser));
            SignInManager
                .Setup(s => s.CheckPasswordSignInAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<bool>()))
                .Returns(Task.FromResult(SignInResult.Success));
            Mediator.Setup(m => m.Send(It.IsAny<GenerateLoginTokenQuery>(), default(CancellationToken))).Returns(Task.FromResult(token));
            // Act
            var returnedToken = Handler.Handle(new LoginUserQuery(email, password), default(CancellationToken)).Result;
            // Assert
            Assert.Equal(token, returnedToken);
        }

        [Theory]
        [InlineData("test@test.ca", "Test123!")]
        [InlineData("user@domain.com", "Password!1f4")]
        public async Task LoginUser_ThrowsInvalidCredentialExceptionWhenInvalidCredentials(string email, string password)
        {
            // Arrange
            var requestedUser = new ApplicationUser()
            {
                Email = email
            };
            Mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), default(CancellationToken)))
                .ReturnsAsync(requestedUser);
            SignInManager
                .Setup(s => s.CheckPasswordSignInAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<bool>()))
                .ReturnsAsync(SignInResult.Failed);
            UserManager.Setup(u => u.IsEmailConfirmedAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(true);
            // Act / Assert
            await Assert.ThrowsAsync<InvalidCredentialException>(() =>
                Handler.Handle(new LoginUserQuery(email, password), CancellationToken.None));
        }

        [Theory]
        [InlineData("test@test.ca", "Test123!")]
        [InlineData("user@domain.com", "Password!1f4")]
        public async Task LoginUser_ThrowsInvalidCredentialExceptionWhenUserNotFound(string email, string password)
        {
            // Arrange
            Mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), default(CancellationToken))).Returns(Task.FromResult((ApplicationUser) null));
            // Act / Assert
            await Assert.ThrowsAsync<InvalidCredentialException>(() => Handler.Handle(new LoginUserQuery(email, password), CancellationToken.None));
        }

        [Theory]
        [InlineData("test@test.ca", "Test123!")]
        [InlineData("user@domain.com", "Password!1f4")]
        public async Task LoginUser_ThrowsAccountLockedException(string email, string password)
        {
            // Arrange
            var requestedUser = new ApplicationUser()
            {
                Email = email
            };
            Mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), default(CancellationToken))).ReturnsAsync(requestedUser);
            SignInManager
                .Setup(s => s.CheckPasswordSignInAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<bool>()))
                .ReturnsAsync(SignInResult.LockedOut);
            // Act / Assert
            // Act / Assert
            await Assert.ThrowsAsync<AccountLockedException>(() =>
                Handler.Handle(new LoginUserQuery(email, password), CancellationToken.None));
        }
        [Theory]
        [InlineData("test@test.ca", "Test123!")]
        [InlineData("user@domain.com", "Password!1f4")]
        public async Task LoginUser_ThrowsEmailNotConfirmedException(string email, string password)
        {
            // Arrange
            var requestedUser = new ApplicationUser()
            {
                Email = email
            };
            Mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), default(CancellationToken)))
                .ReturnsAsync(requestedUser);
            SignInManager
                .Setup(s => s.CheckPasswordSignInAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<bool>()))
                .ReturnsAsync(SignInResult.Failed);
            UserManager.Setup(u => u.IsEmailConfirmedAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(false);
            // Act / Assert
            await Assert.ThrowsAsync<EmailNotConfirmedException>(() =>
                Handler.Handle(new LoginUserQuery(email, password), CancellationToken.None));
        }
    }
}