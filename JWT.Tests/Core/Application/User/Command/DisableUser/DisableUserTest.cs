using System.Threading;
using System.Threading.Tasks;
using JWT.Application.User.Command.DisableUser;
using JWT.Application.User.Query.GetUserById;
using JWT.Domain.Entities;
using JWT.Domain.Exceptions;
using JWT.Persistence;
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
            Mediator.Setup(m => m.Send(It.IsAny<GetUserByIdQuery>(), CancellationToken.None)).ReturnsAsync((ApplicationUser) null);
            // Act / Assert
            await Assert.ThrowsAsync<InvalidUserException>(() => Handler.Handle(new DisableUserCommand(userId), CancellationToken.None));
        }
    }
}
