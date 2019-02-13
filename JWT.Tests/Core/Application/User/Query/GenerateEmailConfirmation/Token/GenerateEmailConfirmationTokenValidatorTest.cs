using System.Linq;
using JWT.Application.User.Query.GenerateEmailConfirmation.Token;
using JWT.Domain.Entities;
using Xunit;

namespace JWT.Tests.Core.Application.User.Query.GenerateEmailConfirmation.Token
{
    public class GenerateEmailConfirmationTokenValidatorTest
    {
        public GenerateEmailConfirmationTokenQueryValidator Validator { get; }
        public GenerateEmailConfirmationTokenValidatorTest()
        {
            // Arrange
            Validator = new GenerateEmailConfirmationTokenQueryValidator();
        }

        [Theory]
        [InlineData("test@test.ca")]
        [InlineData("user@domain.com")]
        public void GenerateEmailConfirmationToken_EmailIsValid(string email)
        {
            var user = new ApplicationUser()
            {
                Email = email
            };
            // Act
            var result = Validator.Validate(new GenerateEmailConfirmationTokenQuery(user));
            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void GenerateEmailConfirmationToken_EmailIsInvalid()
        {
            // Act
            var result = Validator.Validate(new GenerateEmailConfirmationTokenQuery(null));
            // Assert
            Assert.Contains("User required", result.Errors.First().ErrorMessage);
            Assert.False(result.IsValid);
        }
    }
}
