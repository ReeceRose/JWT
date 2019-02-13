using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JWT.Application.User.Command.UpdateUser;
using JWT.Domain.Exceptions;
using JWT.Persistence;
using JWT.Tests.Context;
using Xunit;

namespace JWT.Tests.Core.Application.User.Command.UpdateUser
{
    public class UpdateUserTest : IDisposable
    {
        public ApplicationDbContext Context { get; }
        public UpdateUserCommandHandler Handler { get; }

        public UpdateUserTest()
        {
            // Arrange
            Context = ContextFactory.Create();
            Handler = new UpdateUserCommandHandler(Context);
        }

        [Fact]
        public async Task UpdateUser_HandlesNull()
        {
            // Act / Assert
            await Assert.ThrowsAsync<InvalidUserException>(() => Handler.Handle(new UpdateUserCommand(null), CancellationToken.None));
        }

        [Theory]
        [InlineData("test@test.ca", "user@domain.com")]
        public async Task UpdateUser_UpdatesUser(string email, string newEmail)
        {
            var user = Context.Users.First(u => u.Email == email);
            user.Email = newEmail;
            // Act
            var result = await Handler.Handle(new UpdateUserCommand(user), CancellationToken.None);
            // Assert
            Assert.True(result);
            Assert.Equal(Context.Users.First().Email, newEmail);
        }

        public void Dispose()
        {
            ContextFactory.Destroy(Context);
        }
    }
}
