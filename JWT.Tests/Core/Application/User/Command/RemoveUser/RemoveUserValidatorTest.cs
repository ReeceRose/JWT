using JWT.Application.User.Command.RemoveUser;
using Xunit;

namespace JWT.Tests.Core.Application.User.Command.RemoveUser
{
    public class RemoveUserValidatorTest
    {
        public RemoveUserCommandValidator Validator { get; }

        public RemoveUserValidatorTest()
        {
            // Arrange
            Validator = new RemoveUserCommandValidator();
        }

        [Theory]
        [InlineData("1234567890")]
        [InlineData("0987654321")]
        public void RemoveUser_UserIdIsValid(string userId)
        {
            // Act
            var result = Validator.Validate(new RemoveUserCommand(userId));
            // Assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void RemoveUser_UserIdIsInvalid(string userId)
        {
            // Act
            var result = Validator.Validate(new RemoveUserCommand(userId));
            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("User ID required", result.Errors[0].ErrorMessage);
        }
    }
}
