using System.Threading;
using System.Threading.Tasks;
using JWT.Application.User.Command.ConfirmUserEmail;
using JWT.Application.User.Query.GetUserById;
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

        public ConfirmUserEmailTest()
        {
            // Arrange
            Mediator = new Mock<IMediator>();
            UserManager = new Mock<MockUserManager>();
            Handler = new ConfirmUserEmailCommandHandler(Mediator.Object, UserManager.Object);
        }

        [Theory]
        [InlineData("1234234", "0954368")]
        [InlineData("0954368", "1234234")]
        public void ConfirmUserEmail_SuccessfullyConfirmsEmail(string userId, string token)
        {
            // Arrange
            var requestedUser = new IdentityUser()
            {
                Id =  userId
            };
            Mediator.Setup(m => m.Send(It.IsAny<GetUserByIdQuery>(), default(CancellationToken))).Returns(Task.FromResult(requestedUser));
            UserManager.Setup(u => u.ConfirmEmailAsync(It.IsAny<IdentityUser>(), It.IsAny<string>())).Returns(Task.FromResult(IdentityResult.Success));
            var query = new ConfirmUserEmailCommand()
            {
                UserId = userId,
                Token = token
            };
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
            Mediator.Setup(m => m.Send(It.IsAny<GetUserByIdQuery>(), default(CancellationToken))).Returns(Task.FromResult((IdentityUser) null));
            var query = new ConfirmUserEmailCommand()
            {
                UserId = userId,
                Token = token
            };
            // Act / Assert
            await Assert.ThrowsAsync<InvalidUserException>(() => Handler.Handle(query, CancellationToken.None));
        }
    }
}
