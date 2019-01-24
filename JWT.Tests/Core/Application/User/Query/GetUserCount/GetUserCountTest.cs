using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JWT.Application.User.Model;
using JWT.Application.User.Query.GetAllUsers;
using JWT.Application.User.Query.GetUserCount;
using MediatR;
using Moq;
using Xunit;

namespace JWT.Tests.Core.Application.User.Query.GetUserCount
{
    public class GetUserCountTest
    {
        public Mock<IMediator> Mediator { get; }
        public GetUserCountQueryHandler Handler { get; }

        public GetUserCountTest()
        {
            // Arrange
            Mediator = new Mock<IMediator>();
            Handler = new GetUserCountQueryHandler(Mediator.Object);
        }

        [Fact]
        public async Task GetUserCount_ReturnsZero()
        {
            // Arrange
            Mediator.Setup(m => m.Send(It.IsAny<GetAllUsersQuery>(), default(CancellationToken))).ReturnsAsync((List<ApplicationUserDto>) null);
            // Act
            var result = await Handler.Handle(new GetUserCountQuery(), CancellationToken.None);
            // Assert
            Assert.Equal(0, result);
        }
        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(10000)]
        public async Task GetUserCount_ReturnsValidCount(int userCount)
        {
            // Arrange
            var users = new List<ApplicationUserDto>();
            for (var i = 0; i < userCount; i++)
            {
                users.Add(new ApplicationUserDto());
            }

            Mediator.Setup(m => m.Send(It.IsAny<GetAllUsersQuery>(), default(CancellationToken))).ReturnsAsync(users);
            // Act
            var result = await Handler.Handle(new GetUserCountQuery(), CancellationToken.None);
            // Assert
            Assert.Equal(result, users.Count);
        }
    }
}
