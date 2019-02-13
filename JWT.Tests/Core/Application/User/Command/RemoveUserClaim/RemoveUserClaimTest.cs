using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using JWT.Application.User.Command.RemoveUserClaim;
using JWT.Domain.Entities;
using JWT.Tests.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace JWT.Tests.Core.Application.User.Command.RemoveUserClaim
{
    public class RemoveUserClaimTest
    {
        public Mock<MockUserManager> UserManager { get; }
        public Mock<ILogger<RemoveUserClaimCommandHandler>> Logger { get; }
        public RemoveUserClaimCommandHandler Handler { get; }

        public RemoveUserClaimTest()
        {
            // Arrange
            UserManager = new Mock<MockUserManager>();
            Logger = new Mock<ILogger<RemoveUserClaimCommandHandler>>();
            Handler = new RemoveUserClaimCommandHandler(UserManager.Object, Logger.Object);
        }

        [Theory]
        [InlineData("user@test.com", "key")]
        [InlineData("user@test.com", "test")]
        public async Task RemoveUserClaim_ClaimAdded(string email, string key)
        {
            // Arrange
            var user = new ApplicationUser()
            {
                Email = email
            };
            UserManager.Setup(u => u.RemoveClaimAsync(It.IsAny<ApplicationUser>(), It.IsAny<Claim>())).ReturnsAsync(IdentityResult.Success);
            // Act
            var result = await Handler.Handle(new RemoveUserClaimCommand(user, key), CancellationToken.None);
            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData("user@test.com", null)]
        public async Task RemoveUserClaim_ClaimNotAdded(string email, string key)
        {
            // Arrange
            var user = new ApplicationUser()
            {
                Email = email
            };
            UserManager.Setup(u => u.RemoveClaimAsync(It.IsAny<ApplicationUser>(), It.IsAny<Claim>())).ReturnsAsync(IdentityResult.Failed());
            // Act / Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => Handler.Handle(new RemoveUserClaimCommand(user, key), CancellationToken.None));
        }
    }
}
