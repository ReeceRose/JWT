using System.Linq;
using JWT.Application.User.Command.RegenerateConfirmationEmail;
using Xunit;

namespace JWT.Tests.Core.Application.User.Command.RegenerateConfirmationEmail
{
    public class RegenerateConfirmationEmailTest
    {
        public RegenerateConfirmationEmailCommandValidator Validator { get; }
        public RegenerateConfirmationEmailTest()
        {
            Validator = new RegenerateConfirmationEmailCommandValidator();
        }

        [Theory]
        [InlineData("test@test.ca")]
        [InlineData("user@domain.com")]
        public void RegenerateConfirmationEmail_EmailIsValid(string email)
        {
            var result = Validator.Validate(new RegenerateConfirmationEmailCommand(email));
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("test.ca")]
        public void RegenerateConfirmationEmail_EmailIsInvalid(string email)
        {
            var result = Validator.Validate(new RegenerateConfirmationEmailCommand(email));
            Assert.Contains("Email is required", result.Errors.First().ErrorMessage);
            Assert.False(result.IsValid);
        }
    }
}
