using System.Threading;
using System.Threading.Tasks;
using JWT.Application.User.Command.DisableUser;
using JWT.Application.User.Command.UpdateUser;
using JWT.Application.User.Query.GetUserById;
using JWT.Domain.Entities;
using JWT.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace JWT.Tests.Core.Application.User.Command.DisableUser
{
    public class DisableUserTest
    {
        public Mock<IMediator> Mediator { get; }
        public Mock<ILogger<DisableUserCommandHandler>> Logger { get; }
        public DisableUserCommandHandler Handler { get; }

        public DisableUserTest()
        {
            // Arrange
            Mediator = new Mock<IMediator>();
            Logger = new Mock<ILogger<DisableUserCommandHandler>>();
            Handler = new DisableUserCommandHandler(Mediator.Object, Logger.Object);
        }

        [Theory]
        [InlineData("1234567890")]
        [InlineData("0987654321")]
        public async Task DisableUser_ThrowsInvalidUserException(string userId)
        {
            // Arrange
            Mediator.Setup(m => m.Send(It.IsAny<GetUserByIdQuery>(), default(CancellationToken))).ReturnsAsync((ApplicationUser) null);
            // Act / Assert
            await Assert.ThrowsAsync<InvalidUserException>(() => Handler.Handle(new DisableUserCommand(userId), CancellationToken.None));
        }

        [Theory]
        [InlineData("1234567890", "user@domain.com")]
        [InlineData("0987654321", "test@test.ca")]
        public async Task DisableUser_ReturnsTrue(string userId, string email)
        {
            var requestedUser = new ApplicationUser()
            {
                Id = userId,
                Email = email,
                AccountEnabled = false
            };
            // Arrange
            Mediator.Setup(m => m.Send(It.IsAny<GetUserByIdQuery>(), default(CancellationToken))).ReturnsAsync(requestedUser);
            Mediator.Setup(m => m.Send(It.IsAny<UpdateUserCommand>(), default(CancellationToken))).ReturnsAsync(true);
            // Act
            var result = await Handler.Handle(new DisableUserCommand(userId), CancellationToken.None);
            // Assert
            Assert.True(result);
        }
    }
}
