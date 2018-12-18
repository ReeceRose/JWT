using System.Linq;
using JWT.Application.User.Query.GenerateResetPassword.Email;
using Xunit;

namespace JWT.Tests.Core.Application.User.Query.GenerateResetPassword.Email
{
    public class GenerateResetPasswordEmailValidationTest
    {
        public GenerateResetPasswordEmailQueryValidator Validator { get; }
        public GenerateResetPasswordEmailValidationTest()
        {
            // Arrange
            Validator = new GenerateResetPasswordEmailQueryValidator();
        }

        [Theory]
        [InlineData("test@test.ca")]
        [InlineData("user@domain.com")]
        public void RegenerateConfirmationEmail_EmailIsValid(string email)
        {
            // Act
            var result = Validator.Validate(new GenerateResetPasswordEmailQuery(email));
            // Assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("test.ca")]
        public void RegenerateConfirmationEmail_EmailIsInvalid(string email)
        {
            // Act
            var result = Validator.Validate(new GenerateResetPasswordEmailQuery(email));
            // Assert
            Assert.Contains("Email is required", result.Errors.First().ErrorMessage);
            Assert.False(result.IsValid);
        }
    }
}