using System;
using System.Threading;
using JWT.Application.User.Query.GetUserByEmail;
using JWT.Domain.Entities;
using JWT.Persistence;
using JWT.Tests.Context;
using Xunit;

namespace JWT.Tests.Core.Application.User.Query.GetUserByEmail
{
    public class GetUserByEmailTest : IDisposable
    {
        public ApplicationDbContext Context { get; }
        public GetUserByEmailQueryHandler Handler { get; }

        public GetUserByEmailTest()
        {
            // Arrange
            Context = ContextFactory.Create();
            Handler = new GetUserByEmailQueryHandler(Context);
        }

        [Theory]
        [InlineData("test@test.ca", "test-user", "123")]
        public void GetUserByEmail_ReturnsExpectedUser(string email, string userName, string id)
        {
            // Arrange
            var requestedUser = new ApplicationUser()
            {
                Email = email,
                UserName = userName,
                Id = id
            };
            // Act
            var returnedUser = Handler.Handle(new GetUserByEmailQuery(requestedUser.Email), CancellationToken.None).Result;
            // Assert
            Assert.Equal(requestedUser.Email, returnedUser.Email);
            Assert.Equal(requestedUser.UserName, returnedUser.UserName);
            Assert.Equal(requestedUser.Id, returnedUser.Id);
        }

        [Fact]
        public void GetUserByEmail_NullUserReturnsNull()
        {
            // Act
            var returnedUser = Handler.Handle(new GetUserByEmailQuery(null), CancellationToken.None).Result;
            // Assert
            Assert.Null(returnedUser);
        }

        [Theory]
        [InlineData("user@test.ca")]
        [InlineData("user@domain.com")]
        public void GetUserByEmail_InvalidUserReturnsNull(string email)
        {
            // Act
            var returnedUser = Handler.Handle(new GetUserByEmailQuery(email), CancellationToken.None).Result;
            // Assert
            Assert.Null(returnedUser);
        }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}