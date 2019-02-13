using System.Threading;
using System.Threading.Tasks;
using JWT.Application.User.Command.ConfirmUserEmail;
using JWT.Application.User.Command.ForceEmailConfirmation;
using JWT.Application.User.Query.GenerateEmailConfirmation.Token;
using JWT.Application.User.Query.GetUserById;
using JWT.Domain.Entities;
using JWT.Domain.Exceptions;
using JWT.Tests.Helpers;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace JWT.Tests.Core.Application.User.Command.ForceEmailConfirmation
{
    public class ForceEmailConfirmationTest
    {
        public Mock<IMediator> Mediator { get; }
        public Mock<MockUserManager> UserManager { get; }
        public Mock<ILogger<ForceEmailConfirmationCommandHandler>> Logger { get; }
        public ForceEmailConfirmationCommandHandler Handler { get; }
        
        public ForceEmailConfirmationTest()
        {
            // Arrange
            Mediator = new Mock<IMediator>();
            UserManager = new Mock<MockUserManager>();
            Logger = new Mock<ILogger<ForceEmailConfirmationCommandHandler>>();
            Handler = new ForceEmailConfirmationCommandHandler(Mediator.Object, UserManager.Object, Logger.Object);
        }

        [Theory]
        [InlineData("1234567890")]
        [InlineData("0987654321")]
        public async Task ForceEmailConfirmation_ThrowsInvalidUserException(string userId)
        {
            // Arrange
            Mediator.Setup(m => m.Send(It.IsAny<GetUserByIdQuery>(), default(CancellationToken))).ReturnsAsync((ApplicationUser) null);
            // Act / Assert
            await Assert.ThrowsAsync<InvalidUserException>(() => Handler.Handle(new ForceEmailConfirmationCommand(userId), CancellationToken.None));
        }

        [Theory]
        [InlineData("1234567890", "user@domain.com")]
        [InlineData("0987654321", "test@test.ca")]
        public async Task ForceEmailConfirmation_ThrowsEmailIsAlreadyConfirmedException(string userId, string email)
        {
            // Arrange
            var requestedUser = new ApplicationUser()
            {
                Id = userId,
                Email = email
            };
            Mediator.Setup(m => m.Send(It.IsAny<GetUserByIdQuery>(), default(CancellationToken))).ReturnsAsync(requestedUser);
            UserManager.Setup(u => u.IsEmailConfirmedAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(true);
            // Act / Assert
            await Assert.ThrowsAsync<EmailIsAlreadyConfirmedException>(() => Handler.Handle(new ForceEmailConfirmationCommand(userId), CancellationToken.None));
        }

        [Theory]
        [InlineData("1234567890", "user@domain.com", "0987654321")]
        [InlineData("0987654321", "test@test.ca", "12345678980")]
        public async Task ForceEmailConfirmation_ReturnsTrue(string userId, string email, string token)
        {
            // Arrange
            var requestedUser = new ApplicationUser()
            {
                Id = userId,
                Email = email
            };
            Mediator.Setup(m => m.Send(It.IsAny<GetUserByIdQuery>(), default(CancellationToken))).ReturnsAsync(requestedUser);
            UserManager.Setup(u => u.IsEmailConfirmedAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(false);
            Mediator.Setup(m => m.Send(It.IsAny<GenerateEmailConfirmationTokenQuery>(), default(CancellationToken))).ReturnsAsync(token);
            Mediator.Setup(m => m.Send(It.IsAny<ConfirmUserEmailCommand>(), default(CancellationToken))).ReturnsAsync(true);
            // Act
            var result = await Handler.Handle(new ForceEmailConfirmationCommand(userId), CancellationToken.None);
            // Assert
            Assert.True(result);
        }
    }
}
