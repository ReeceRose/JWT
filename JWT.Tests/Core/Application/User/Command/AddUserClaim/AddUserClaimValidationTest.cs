using System.Linq;
using JWT.Application.User.Command.AddUserClaim;
using JWT.Domain.Entities;
using Xunit;

namespace JWT.Tests.Core.Application.User.Command.AddUserClaim
{
    public class AddUserClaimValidationTest
    {
        public AddUserClaimCommandValidator Validator { get; set; }

        public AddUserClaimValidationTest()
        {
            // Arrange
            Validator = new AddUserClaimCommandValidator();;
        }

        [Theory]
        [InlineData("test-user")]
        [InlineData("user@domain.com")]
        public void GetUserClaim_UserIsValid(string userName)
        {
            // Arrange
            var user = new ApplicationUser()
            {
                UserName = userName,
                Id = "123"
            };
            // Act
            var result = Validator.Validate(new AddUserClaimCommand(user, "key", "value"));
            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void GetUserClaim_UserIsInvalid()
        {
            // Act
            var result = Validator.Validate(new AddUserClaimCommand(null, "key", "value"));
            // Assert
            Assert.Contains("User required", result.Errors.First().ErrorMessage);
            Assert.False(result.IsValid);
        }

        [Theory]
        [InlineData("key", "value")]
        [InlineData("role", "Administrator")]
        public void AddUserClaim_KeyIsValid(string key, string value)
        {
            // Arrange
            var user = new ApplicationUser()
            {
                Email = "user@test.com",
                Id = "123"
            };
            // Act
            var result = Validator.Validate(new AddUserClaimCommand(user, key, value));
            // Assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData("", "value")]
        [InlineData(null, "value")]
        public void AddUserClaim_KeyIsInvalid(string key, string value)
        {
            // Arrange
            var user = new ApplicationUser()
            {
                UserName = "test-user",
                Id = "123"
            };
            // Act
            var result = Validator.Validate(new AddUserClaimCommand(user, key, value));
            // Assert
            Assert.Contains("Key required", result.Errors.First().ErrorMessage);
            Assert.False(result.IsValid);
        }

        [Theory]
        [InlineData("key", "value")]
        [InlineData("role", "Administrator")]
        public void AddUserClaim_ValueIsValid(string key, string value)
        {
            // Arrange
            var user = new ApplicationUser()
            {
                UserName = "test-user",
                Id = "123"
            };
            // Act
            var result = Validator.Validate(new AddUserClaimCommand(user, key, value));
            // Assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData("key", "")]
        [InlineData("key", null)]
        public void AddUserClaim_ValueIsInvalid(string key, string value)
        {
            // Arrange
            var user = new ApplicationUser()
            {
                UserName = "test-user",
                Id = "123"
            };
            // Act
            var result = Validator.Validate(new AddUserClaimCommand(user, key, value));
            // Assert
            Assert.Contains("Value required", result.Errors.First().ErrorMessage);
            Assert.False(result.IsValid);
        }
    }
}
