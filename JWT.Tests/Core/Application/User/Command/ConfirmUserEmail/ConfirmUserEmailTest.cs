using System.Linq;
using JWT.Application.User.Command.ConfirmUserEmail;
using Xunit;

namespace JWT.Tests.Core.Application.User.Command.ConfirmUserEmail
{
    public class ConfirmUserEmailTest
    {
        public ConfirmUserEmailCommandValidator Validator { get; }

        public ConfirmUserEmailTest()
        {
            // Arrange
            Validator = new ConfirmUserEmailCommandValidator();
        }

        [Theory]
        [InlineData("1234567890")]
        [InlineData("0")]
        public void ConfirmationUserEmail_UserIdIsValid(string userId)
        {
            // Act
            var result = Validator.Validate(new ConfirmUserEmailCommand() {Token = "1", UserId = userId});
            // Assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ConfirmationUserEmail_UserIdIsInvalid(string userId)
        {
            // Act
            var result = Validator.Validate(new ConfirmUserEmailCommand() { Token = "1", UserId = userId });
            // Assert
            Assert.Contains("User ID required", result.Errors.First().ErrorMessage);
            Assert.False(result.IsValid);
        }

        [Theory]
        [InlineData("1234567890")]
        [InlineData("0")]
        public void ConfirmationUserEmail_TokenIsValid(string token)
        {
            // Act
            var result = Validator.Validate(new ConfirmUserEmailCommand() {Token = token, UserId = "1"});
            // Assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ConfirmationUserEmail_TokenIsInvalid(string token)
        {
            // Act
            var result = Validator.Validate(new ConfirmUserEmailCommand() { Token = token, UserId = "1" });
            // Assert
            Assert.Contains("Token required", result.Errors.First().ErrorMessage);
            Assert.False(result.IsValid);
        }
    }
}