using System;
using System.Threading;
using AutoMapper;
using JWT.Application.User.Query.GetUserByEmail;
using JWT.Application.Utilities;
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

        [Fact]
        public void GetUserByEmail_ReturnsExpectedUser()
        {
            // Arrange
            var requestedUser = new ApplicationUser()
            {
                Email = "test@test.ca",
                UserName = "test-user",
                Id = "123"
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