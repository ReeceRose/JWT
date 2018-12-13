using System.Threading;
using JWT.Application.User.Query.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace JWT.Tests.Core.Application.User.Query.GetUserById
{
    public class GetUserByIdTest
    {
        public Mock<IMediator> Mediator { get; }

        public GetUserByIdTest()
        {
            // Arrange
            Mediator = new Mock<IMediator>();   
        }

        [Theory]
        [InlineData("123", "email@test.ca", "username")]
        [InlineData("324231", "your@domain.com", "username")]
        public void GetUserById_(string id, string email, string username)
        {
            // Arrange
            var requestedUser = new IdentityUser()
            {
                Id = id,
                Email = email,
                UserName = username
            };
            var query = new GetUserByIdQuery(requestedUser.Id);
            Mediator.Setup(m => m.Send(It.IsAny<GetUserByIdQuery>(), default(CancellationToken))).ReturnsAsync(requestedUser);
            // Act
            var returnedUser = Mediator.Object.Send(query).Result;
            // Assert
            Assert.Equal(requestedUser.Id, returnedUser.Id);
            Assert.Equal(requestedUser.Email, returnedUser.Email);
            Assert.Equal(requestedUser.UserName, returnedUser.UserName);
        }

        [Fact]
        public void GetUserByEmail_NullUSerReturnsNull()
        {
            // Arrange
            var query = new GetUserByIdQuery(null);
            Mediator.Setup(m => m.Send(It.IsAny<GetUserByIdQuery>(), default(CancellationToken))).ReturnsAsync((IdentityUser)null);
            // Act
            var result = Mediator.Object.Send(query).Result;
            // Assert
            Assert.Null(result);
        }
        // Invalid user returns null
        [Theory]
        [InlineData("123", "email@test.ca", "username")]
        [InlineData("324231", "your@domain.com", "username")]
        public void GetUserByEmail_InvalidUserReturnsNull(string id, string email, string username)
        {
            // Arrange
            var requestedUser = new IdentityUser()
            {
                Id = id,
                Email = email,
                UserName = username
            };
            var query = new GetUserByIdQuery(requestedUser.Email);
            Mediator.Setup(m => m.Send(It.IsAny<GetUserByIdQuery>(), default(CancellationToken))).ReturnsAsync((IdentityUser)null);
            // Act
            var result = Mediator.Object.Send(query).Result;
            // Assert
            Assert.Null(result);
        }
    }
}
