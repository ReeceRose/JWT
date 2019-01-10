using System.Linq;
using JWT.Application.User.Command.ResetPassword;
using Xunit;

namespace JWT.Tests.Core.Application.User.Command.ResetPassword
{
    public class ResetPasswordValidationTest
    {
        public ResetPasswordCommandValidator Validator { get; }
        public ResetPasswordValidationTest()
        {
            // Arrange
            Validator = new ResetPasswordCommandValidator();
        }

        [Theory]
        [InlineData("123123")]
        [InlineData("321312")]
        public void ResetPassword_TokenIsValid(string token)
        {
            // Act
            var result = Validator.Validate(new ResetPasswordCommand(email: "user@domain.com", password: "Test1!", token: token));
            // Assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ResetPassword_TokenIsInvalid(string token)
        {
            // Act
            var result = Validator.Validate(new ResetPasswordCommand(email: "user@domain.com", password: "Test1!", token: token));
            // Assert
            Assert.Contains("Token required", result.Errors.First().ErrorMessage);
            Assert.False(result.IsValid);
        }

        [Theory]
        [InlineData("test@test.ca")]
        [InlineData("user@domain.com")]
        public void ResetPassword_EmailIsValid(string email)
        {
            // Act
            var result = Validator.Validate(new ResetPasswordCommand(email: email, password: "Test1!", token: "123"));
            // Assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("test.ca")]
        public void ResetPassword_EmailIsInvalid(string email)
        {
            // Act
            var result = Validator.Validate(new ResetPasswordCommand(email: email, password: "Test1!", token: "123"));
            // Assert
            Assert.Contains("Email is required", result.Errors.First().ErrorMessage);
            Assert.False(result.IsValid);
        }

        [Theory]
        [InlineData("Test123!")]
        [InlineData("Password!1f4")]
        public void ResetPassword_PasswordIsValid(string password)
        {
            // Act
            var result = Validator.Validate(new ResetPasswordCommand(email: "test@test.ca", password: password, token: "123"));
            // Assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData("")] // Empty
        [InlineData(null)] // Null
        [InlineData("T1!")] // Too few characters
        [InlineData("Test12")] // Does not contain any special characters
        [InlineData("Test###")] // Does not contain any numbers
        public void ResetPassword_PasswordIsInvalid(string password)
        {
            // Act
            var result = Validator.Validate(new ResetPasswordCommand(email: "test@test.ca", password: password, token: "123"));
            // Assert
            Assert.Contains("Password does not meet security constraints", result.Errors.First().ErrorMessage);
            Assert.False(result.IsValid);
        }
    }
}