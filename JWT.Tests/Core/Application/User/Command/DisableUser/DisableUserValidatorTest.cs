using JWT.Application.User.Command.DisableUser;
using Xunit;

namespace JWT.Tests.Core.Application.User.Command.DisableUser
{
    public class DisableUserValidatorTest
    {
        public DisableUserCommandValidator Validator { get; }

        public DisableUserValidatorTest()
        {
            // Arrange
            Validator = new DisableUserCommandValidator();
        }

        [Theory]
        [InlineData("1234567890")]
        [InlineData("0987654321")]
        public void DisableUser_UserIdIsValid(string userId)
        {
            // Act
            var result = Validator.Validate(new DisableUserCommand(userId));
            // Assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void DisableUser_UserIdIsInvalid(string userId)
        {
            // Act
            var result = Validator.Validate(new DisableUserCommand(userId));
            // Assert
            Assert.False(result.IsValid);
        }
    }
}
