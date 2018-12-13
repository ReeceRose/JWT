using System.Threading;
using JWT.Application.User.Query.GetUserByEmail;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace JWT.Tests.Core.Application.User.Query.GetUserByEmail
{
    public class GetUserByEmailTest
    {
        public Mock<IMediator> Mediator { get; }

        public GetUserByEmailTest()
        {
            // Arrange
            Mediator = new Mock<IMediator>();
        }

        [Theory]
        [InlineData("test@test.com", "test-user", "123")]
        [InlineData("user@test.com", "user", "321")]
        // Returns expected user
        public void GetUserByEmail_ReturnsExpectedUser(string email, string username, string id)
        {
            // Arrange
            var requestedUser = new IdentityUser()
            {
                Email = email,
                UserName = username,
                Id = id
            };
            var query = new GetUserByEmailQuery(requestedUser.Email);
            Mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), default(CancellationToken))).ReturnsAsync(requestedUser);
            // Act
            var returnedUser = Mediator.Object.Send(query).Result;
            // Assert
            Assert.Equal(requestedUser.Email, returnedUser.Email);
            Assert.Equal(requestedUser.UserName, returnedUser.UserName);
            Assert.Equal(requestedUser.Id, returnedUser.Id);
        }

        [Fact]
        public void GetUserByEmail_NullUSerReturnsNull()
        {
            // Arrange
            var query = new GetUserByEmailQuery(null);
            Mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), default(CancellationToken))).ReturnsAsync((IdentityUser) null);
            // Act
            var result = Mediator.Object.Send(query).Result;
            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData("test@test.com", "test-user", "123")]
        [InlineData("user@test.com", "user", "321")]
        public void GetUserByEmail_InvalidUserReturnsNull(string email, string username, string id)
        {
            // Arrange
            var requestedUser = new IdentityUser()
            {
                Email = email,
                UserName = username,
                Id = id
            };
            var query = new GetUserByEmailQuery(requestedUser.Email);
            Mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), default(CancellationToken))).ReturnsAsync((IdentityUser) null);
            // Act
            var result = Mediator.Object.Send(query).Result;
            // Assert
            Assert.Null(result);
        }
    }
}