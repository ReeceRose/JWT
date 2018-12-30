using System.Linq;
using JWT.Application.User.Query.LoginUser.External;
using Xunit;

namespace JWT.Tests.Core.Application.User.Query.LoginUser.External
{
    public class LoginUserExternalValidationTest
    {
        public LoginUserExternalQueryValidator Validator { get; }
        public LoginUserExternalValidationTest()
        {
            // Arrange
            Validator = new LoginUserExternalQueryValidator();
        }

        [Theory]
        [InlineData("test@test.ca")]
        [InlineData("user@domain.com")]
        public void LoginUserExternal_EmailIsValid(string email)
        {
            // Act
            var result = Validator.Validate(new LoginUserExternalQuery(email: email, accessToken: "123"));
            // Assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("test.ca")]
        public void LoginUserExternal_EmailIsInvalid(string email)
        {
            // Act
            var result = Validator.Validate(new LoginUserExternalQuery(email: email, accessToken: "123"));
            // Assert
            Assert.Contains("Email is required", result.Errors.First().ErrorMessage);
            Assert.False(result.IsValid);
        }

        [Theory]
        [InlineData("123123")]
        [InlineData("321312")]
        public void LoginUserExternal_TokenIsValid(string token)
        {
            // Act
            var result = Validator.Validate(new LoginUserExternalQuery(accessToken: token, email: "test@test.com"));
            // Assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void LoginUserExternal_TokenIsInvalid(string token)
        {
            // Act
            var result = Validator.Validate(new LoginUserExternalQuery(accessToken: token, email: "test@test.com"));
            // Assert
            Assert.Contains("Access token required", result.Errors.First().ErrorMessage);
            Assert.False(result.IsValid);
        }

    }
}