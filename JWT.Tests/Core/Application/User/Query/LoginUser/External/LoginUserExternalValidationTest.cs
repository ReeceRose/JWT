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
        [InlineData("123123")]
        [InlineData("321312")]
        public void LoginUserExternal_TokenIsValid(string token)
        {
            // Act
            var result = Validator.Validate(new LoginUserExternalQuery(accessToken: token));
            // Assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void LoginUserExternal_TokenIsInvalid(string token)
        {
            // Act
            var result = Validator.Validate(new LoginUserExternalQuery(accessToken: token));
            // Assert
            Assert.Contains("Access token required", result.Errors.First().ErrorMessage);
            Assert.False(result.IsValid);
        }
    }
}
