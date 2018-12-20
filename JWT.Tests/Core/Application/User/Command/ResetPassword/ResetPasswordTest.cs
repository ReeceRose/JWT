using System.Threading;
using System.Threading.Tasks;
using JWT.Application.User.Command.ResetPassword;
using JWT.Application.User.Query.GetUserByEmail;
using JWT.Domain.Exceptions;
using JWT.Tests.Helpers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace JWT.Tests.Core.Application.User.Command.ResetPassword
{
    public class ResetPasswordTest
    {
        public Mock<IMediator> Mediator { get; }
        public Mock<MockUserManager> UserManager { get; }
        public ResetPasswordCommandHandler Handler { get; }

        public ResetPasswordTest()
        {
            // Arrange
            Mediator = new Mock<IMediator>();
            UserManager = new Mock<MockUserManager>();
            Handler = new ResetPasswordCommandHandler(Mediator.Object, UserManager.Object);
        }

        // Valid
        [Theory]
        [InlineData("test@test.ca", "123456789", "Test1!")]
        [InlineData("user@domain.com", "987654321", "Password1!")]
        public void ResetPassword_ValidPasswordReset(string email, string token, string newPassword)
        {
            // Arrange
            var requestedUser = new IdentityUser()
            {
                Email = email
            };
            Mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), default(CancellationToken))).ReturnsAsync(requestedUser);
            UserManager.Setup(u => u.ResetPasswordAsync(It.IsAny<IdentityUser>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            // Act
            var result = Handler.Handle(new ResetPasswordCommand(token, email, newPassword), CancellationToken.None).Result;
            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData("test@test.ca", "123456789", "Test1!")]
        [InlineData("user@domain.com", "987654321", "Password1!")]
        public async Task ResetPassword_InvalidUser(string email, string token, string newPassword)
        {
            // Arrange
            Mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), default(CancellationToken))).ReturnsAsync((IdentityUser) null);
            // Act / Assert
            await Assert.ThrowsAsync<InvalidUserException>(() => Handler.Handle(new ResetPasswordCommand(token, email, newPassword), CancellationToken.None));
        }

        [Theory]
        [InlineData("test@test.ca", "123456789", "Test1!")]
        [InlineData("user@domain.com", "987654321", "Password1!")]
        public async Task ResetPassword_ThrowsFailedToResetPasswordException(string email, string token, string newPassword)
        {
            // Arrange
            var requestedUser = new IdentityUser()
            {
                Email = email
            };
            Mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), default(CancellationToken))).ReturnsAsync(requestedUser);
            UserManager.Setup(u => u.ResetPasswordAsync(It.IsAny<IdentityUser>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Failed());
            // Act / Assert
            await Assert.ThrowsAsync<FailedToResetPassword>(() => Handler.Handle(new ResetPasswordCommand(email, token, newPassword), CancellationToken.None));
        }
    }
}