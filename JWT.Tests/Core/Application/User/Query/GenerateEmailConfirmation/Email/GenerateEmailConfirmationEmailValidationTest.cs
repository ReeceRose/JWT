using System.Linq;
using JWT.Application.User.Query.GenerateEmailConfirmation.Email;
using Xunit;

namespace JWT.Tests.Core.Application.User.Query.GenerateEmailConfirmation.Email
{
    public class GenerateEmailConfirmationEmailValidationTest
    {
        public GenerateEmailConfirmationEmailQueryValidator Validator { get; }
        public GenerateEmailConfirmationEmailValidationTest()
        {
            // Arrange
            Validator = new GenerateEmailConfirmationEmailQueryValidator();
        }

        [Theory]
        [InlineData("test@test.ca")]
        [InlineData("user@domain.com")]
        public void RegenerateConfirmationEmail_EmailIsValid(string email)
        {
            // Act
            var result = Validator.Validate(new GenerateEmailConfirmationEmailQuery(email));
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
            var result = Validator.Validate(new GenerateEmailConfirmationEmailQuery(email));
            // Assert
            Assert.Contains("Email is required", result.Errors.First().ErrorMessage);
            Assert.False(result.IsValid);
        }
    }
}
