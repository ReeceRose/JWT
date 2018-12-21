using System.Linq;
using JWT.Application.User.Command.RegisterUser;
using JWT.Application.User.Query.LoginUser;
using Xunit;

namespace JWT.Tests.Core.Application.User.Query.LoginUser
{
    public class LoginUserValidationTest
    {
        public LoginUserQueryValidator Validator { get; }
        public LoginUserValidationTest()
        {
            // Arrange
            Validator = new LoginUserQueryValidator();
        }

        [Theory]
        [InlineData("test@test.ca")]
        [InlineData("user@domain.com")]
        public void LoginUser_EmailIsValid(string email)
        {
            // Act
            var result = Validator.Validate(new LoginUserQuery(email: email, password: "Test1!"));
            // Assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("test.ca")]
        public void LoginUser_EmailIsInvalid(string email)
        {
            // Act
            var result = Validator.Validate(new LoginUserQuery(email: email, password: "Test1!"));
            // Assert
            Assert.Contains("Email is required", result.Errors.First().ErrorMessage);
            Assert.False(result.IsValid);
        }

        [Theory]
        [InlineData("Test123!")]
        [InlineData("Password!1f4")]
        public void LoginUser_PasswordIsValid(string password)
        {
            // Act
            var result = Validator.Validate(new LoginUserQuery(email: "test@test.ca", password: password));
            // Assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData("")] // Empty
        [InlineData(null)] // Null
        [InlineData("T1!")] // Too few characters
        [InlineData("Test12")] // Does not contain any special characters
        [InlineData("Test###")] // Does not contain any numbers
        public void LoginUser_PasswordIsInvalid(string password)
        {
            // Act
            var result = Validator.Validate(new LoginUserQuery(email: "test@test.ca", password: password));
            // Assert
            Assert.Contains("Password does not meet security constraints", result.Errors.First().ErrorMessage);
            Assert.False(result.IsValid);
        }
    }
}
