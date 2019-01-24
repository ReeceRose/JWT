using JWT.Application.User.Command.UpdateUser;
using JWT.Domain.Entities;
using Xunit;

namespace JWT.Tests.Core.Application.User.Command.UpdateUser
{
    public class UpdateUserValidatorTest
    {
        public UpdateUserCommandValidator Validator { get; }

        public UpdateUserValidatorTest()
        {
            // Arrange
            Validator = new UpdateUserCommandValidator();
        }

        [Theory]
        [InlineData("1234567890")]
        [InlineData("0987654321")]
        public void UpdateUser_UserIdIsValid(string userId)
        {
            // Arrange
            var user = new ApplicationUser()
            {
                Id = userId
            };
            // Act
            var result = Validator.Validate(new UpdateUserCommand(user));
            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void UpdateUser_UserIdIsInvalid()
        {
            // Act
            var result = Validator.Validate(new UpdateUserCommand(null));
            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("User required", result.Errors[0].ErrorMessage);
        }
    }
}
