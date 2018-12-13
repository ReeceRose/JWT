using System.Threading;
using System.Threading.Tasks;
using JWT.Application.User.Query.LoginUser;
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
        public MockSignInManager SignInManager { get; }
        public MockUserManager UserManager { get; }
        public LoginUserQueryHandler Handler { get; }

        public LoginUserTest()
        {
            // Arrange
            Mediator = new Mock<IMediator>();
            SignInManager = new MockSignInManager();
            UserManager = new MockUserManager();
            Handler = new LoginUserQueryHandler(Mediator.Object, SignInManager, UserManager);
        }

        // Valid
        [Theory]
        [InlineData("email", "password")]
        public void LoginUser_ReturnsValidToken(string email, string password)
        {
            // Arrange
            // TODO: More arrange
            // Act
            var token = Handler.Handle(new LoginUserQuery(email, password), default(CancellationToken)).Result;
            // Assert
        }

        // Throws InvalidCredentialException When null user

        // Throws InvalidCredentialException when invalid credentials

        // Throws AccountLockedException

        // Throws EmailNotConfirmedException
    }
}