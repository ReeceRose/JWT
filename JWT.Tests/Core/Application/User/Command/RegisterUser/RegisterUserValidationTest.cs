using System.Linq;
using JWT.Application.User.Command.RegisterUser;
using Xunit;

namespace JWT.Tests.Core.Application.User.Command.RegisterUser
{
    public class RegisterUserValidationTest
    {
        public RegisterUserCommandValidator Validator { get; }
        public RegisterUserValidationTest()
        {
            // Arrange
            Validator = new RegisterUserCommandValidator();
        }

        [Theory]
        [InlineData("test@test.ca")]
        [InlineData("user@domain.com")]
        public void RegisterUserTest_EmailIsValid(string email)
        {
            // Act
            var result = Validator.Validate(new RegisterUserCommand(email: email, password: "Test1!", isAdmin: false));
            // Assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("test.ca")]
        public void RegisterUserTest_EmailIsInvalid(string email)
        {
            // Act
            var result = Validator.Validate(new RegisterUserCommand(email: email, password: "Test1!", isAdmin: false));
            // Assert
            Assert.Contains("Email is required", result.Errors.First().ErrorMessage);
            Assert.False(result.IsValid);
        }

        [Theory]
        [InlineData("Test1!")]
        [InlineData("ABcd123#!23")]
        public void RegisterUserTest_PasswordIsValid(string password)
        {
            // Act
            var result = Validator.Validate(new RegisterUserCommand(email: "test@test.ca", password: password, isAdmin: false));
            // Assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData("")] // Empty
        [InlineData(null)] // Null
        [InlineData("T1!")] // Too few characters
        [InlineData("Test12")] // Does not contain any special characters
        [InlineData("Test###")] // Does not contain any numbers
        public void RegisterUserTest_PasswordIsInvalid(string password)
        {
            // Act
            var result = Validator.Validate(new RegisterUserCommand(email: "test@test.ca", password: password, isAdmin: false));
            // Assert
            Assert.Contains("Password does not meet security constraints", result.Errors.First().ErrorMessage);
            Assert.False(result.IsValid);
        }
    }
}