using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JWT.Application.User.Query.GetAllUsers;
using JWT.Persistence;
using JWT.Tests.Context;
using Xunit;

namespace JWT.Tests.Core.Application.User.Query.GetAllUsers
{
    public class GetAllUsersTest : IDisposable
    {
        public ApplicationDbContext Context { get; }
        public GetAllUsersQueryHandler Handler { get; }

        public GetAllUsersTest()
        {
            // Arrange
            Context = ContextFactory.Create();
            Handler = new GetAllUsersQueryHandler(Context);
        }

        [Fact]
        public async Task GetAllUsers_ReturnsZero()
        {
            // Arrange
            // Context by default has one user, so remove it
            Context.Remove(Context.Users.First());
            Context.SaveChanges();
            // Act
            var result = await Handler.Handle(new GetAllUsersQuery(), CancellationToken.None);
            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllUsers_ReturnsValidUserCount()
        {
            // Arrange / Act
            var result = await Handler.Handle(new GetAllUsersQuery(), CancellationToken.None);
            // Assert
            Assert.NotEmpty(result);
            Assert.Single(result);
        }

        public void Dispose()
        {
            ContextFactory.Destroy(Context);
        }
    }
}
