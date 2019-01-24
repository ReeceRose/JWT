using JWT.Application.User.Command.DisableUser;
using JWT.Application.User.Command.EnableUser;
using Xunit;

namespace JWT.Tests.Core.Application.User.Command.EnableUser
{
    public class EnableUserValidatorTest
    {
        public EnableUserCommandValidator Validator { get; }

        public EnableUserValidatorTest()
        {
            // Arrange
            Validator = new EnableUserCommandValidator();
        }

        [Theory]
        [InlineData("1234567890")]
        [InlineData("0987654321")]
        public void DisableUser_UserIdIsValid(string userId)
        {
            // Act
            var result = Validator.Validate(new EnableUserCommand(userId));
            // Assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void DisableUser_UserIdIsInvalid(string userId)
        {
            // Act
            var result = Validator.Validate(new EnableUserCommand(userId));
            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("User ID required", result.Errors[0].ErrorMessage);
        }
    }
}
