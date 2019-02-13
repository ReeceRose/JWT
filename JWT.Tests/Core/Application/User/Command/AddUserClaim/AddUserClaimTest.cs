using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using JWT.Application.User.Command.AddUserClaim;
using JWT.Domain.Entities;
using JWT.Tests.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace JWT.Tests.Core.Application.User.Command.AddUserClaim
{
    public class AddUserClaimTest
    {
        public Mock<MockUserManager> UserManager { get; }
        public Mock<ILogger<AddUserClaimCommandHandler>> Logger { get; }
        public AddUserClaimCommandHandler Handler { get; }

        public AddUserClaimTest()
        {
            // Arrange
            UserManager = new Mock<MockUserManager>();
            Logger = new Mock<ILogger<AddUserClaimCommandHandler>>();
            Handler = new AddUserClaimCommandHandler(UserManager.Object, Logger.Object);
        }

        [Theory]
        [InlineData("user@test.com", "key", "value")]
        [InlineData("user@test.com", "test", "claim")]
        public async Task AddUserClaim_ClaimAdded(string email, string key, string value)
        {
            // Arrange
            var user = new ApplicationUser()
            {
                Email = email
            };
            UserManager.Setup(u => u.AddClaimAsync(It.IsAny<ApplicationUser>(), It.IsAny<Claim>())).ReturnsAsync(IdentityResult.Success);
            // Act
            var result = await Handler.Handle(new AddUserClaimCommand(user, key, value), CancellationToken.None);
            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData("user@test.com", null, "value")]
        [InlineData("user@test.com", "key", null)]
        public async Task AddUserClaim_ClaimNotAdded(string email, string key, string value)
        {
            // Arrange
            var user = new ApplicationUser()
            {
                Email = email
            };
            UserManager.Setup(u => u.AddClaimAsync(It.IsAny<ApplicationUser>(), It.IsAny<Claim>())).ReturnsAsync(IdentityResult.Failed());
            // Act / Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => Handler.Handle(new AddUserClaimCommand(user, key, value), CancellationToken.None));
        }
    }
}
