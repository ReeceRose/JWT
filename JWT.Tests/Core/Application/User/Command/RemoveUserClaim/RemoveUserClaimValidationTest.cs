using System.Linq;
using JWT.Application.User.Command.RemoveUserClaim;
using JWT.Domain.Entities;
using Xunit;

namespace JWT.Tests.Core.Application.User.Command.RemoveUserClaim
{
    public class RemoveUserClaimValidationTest
    {
        public RemoveUserClaimValidator Validator { get; set; }

        public RemoveUserClaimValidationTest()
        {
            // Arrange
            Validator = new RemoveUserClaimValidator(); ;
        }

        [Theory]
        [InlineData("test-user")]
        [InlineData("user@domain.com")]
        public void RemoveUserClaim_UserIsValid(string userName)
        {
            // Arrange
            var user = new ApplicationUser()
            {
                UserName = userName,
                Id = "123"
            };
            // Act
            var result = Validator.Validate(new RemoveUserClaimCommand(user, "key"));
            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void RemoveUserClaim_UserIsInvalid()
        {
            // Act
            var result = Validator.Validate(new RemoveUserClaimCommand(null, "key"));
            // Assert
            Assert.Contains("User required", result.Errors.First().ErrorMessage);
            Assert.False(result.IsValid);
        }

        [Theory]
        [InlineData("key")]
        [InlineData("role")]
        public void RemoveUserClaim_KeyIsValid(string key)
        {
            // Arrange
            var user = new ApplicationUser()
            {
                Email = "user@test.com",
                Id = "123"
            };
            // Act
            var result = Validator.Validate(new RemoveUserClaimCommand(user, key));
            // Assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void RemoveUserClaim_KeyIsInvalid(string key)
        {
            // Arrange
            var user = new ApplicationUser()
            {
                UserName = "test-user",
                Id = "123"
            };
            // Act
            var result = Validator.Validate(new RemoveUserClaimCommand(user, key));
            // Assert
            Assert.Contains("Key required", result.Errors.First().ErrorMessage);
            Assert.False(result.IsValid);
        }
    }
}
