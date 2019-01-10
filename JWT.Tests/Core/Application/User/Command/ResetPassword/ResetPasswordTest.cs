using System.Threading;
using System.Threading.Tasks;
using JWT.Application.User.Command.ResetPassword;
using JWT.Application.User.Query.GetUserByEmail;
using JWT.Domain.Entities;
using JWT.Domain.Exceptions;
using JWT.Tests.Helpers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace JWT.Tests.Core.Application.User.Command.ResetPassword
{
    public class ResetPasswordTest
    {
        public Mock<IMediator> Mediator { get; }
        public Mock<MockUserManager> UserManager { get; }
        public Mock<ILogger<ResetPasswordCommandHandler>> Logger { get; set; }
        public ResetPasswordCommandHandler Handler { get; }

        public ResetPasswordTest()
        {
            // Arrange
            Mediator = new Mock<IMediator>();
            UserManager = new Mock<MockUserManager>();
            Logger = new Mock<ILogger<ResetPasswordCommandHandler>>();
            Handler = new ResetPasswordCommandHandler(Mediator.Object, UserManager.Object, Logger.Object);
        }

        // Valid
        [Theory]
        [InlineData("test@test.ca", "", "v6nQZlSB3Ru2ICZBhUA/4g==")]
        [InlineData("user@domain.com", "KwySES16QZ7Jicg8XprasQ==", "Password1!")]
        public void ResetPassword_ValidPasswordReset(string email, string token, string newPassword)
        {
            // Arrange
            var requestedUser = new ApplicationUser()
            {
                Email = email
            };
            Mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), default(CancellationToken))).ReturnsAsync(requestedUser);
            UserManager.Setup(u => u.ResetPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            // Act
            var result = Handler.Handle(new ResetPasswordCommand(token, email, newPassword), CancellationToken.None).Result;
            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData("test@test.ca", "", "v6nQZlSB3Ru2ICZBhUA/4g==")]
        [InlineData("user@domain.com", "KwySES16QZ7Jicg8Xpr+sQ==", "Password1!")]
        public async Task ResetPassword_InvalidUser(string email, string token, string newPassword)
        {
            // Arrange
            Mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), default(CancellationToken))).ReturnsAsync((ApplicationUser) null);
            // Act / Assert
            await Assert.ThrowsAsync<InvalidUserException>(() => Handler.Handle(new ResetPasswordCommand(token, email, newPassword), CancellationToken.None));
        }

        [Theory]
        [InlineData("test@test.ca", "", "v6nQZlSB3Ru2ICZBhUA/4g==")]
        [InlineData("user@domain.com", "KwySES16QZ7Jicg8Xpr+sQ==", "Password1!")]
        public async Task ResetPassword_ThrowsFailedToResetPasswordException(string email, string token, string newPassword)
        {
            // Arrange
            var requestedUser = new ApplicationUser()
            {
                Email = email
            };
            Mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), default(CancellationToken))).ReturnsAsync(requestedUser);
            UserManager.Setup(u => u.ResetPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Failed());
            // Act / Assert
            await Assert.ThrowsAsync<FailedToResetPassword>(() => Handler.Handle(new ResetPasswordCommand(token, email, newPassword), CancellationToken.None));
        }
    }
}