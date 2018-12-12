using System.Linq;
using FluentValidation.TestHelper;
using JWT.Application.User.Command.ConfirmUserEmail;
using Xunit;

namespace JWT.Tests.Core.Application.User.Command.ConfirmUserEmail
{
    public class ConfirmUserEmailTest
    {
        public ConfirmUserEmailCommandValidator Validator { get; }

        public ConfirmUserEmailTest()
        {
            Validator = new ConfirmUserEmailCommandValidator();
        }

        [Theory]
        [InlineData("1234567890")]
        [InlineData("0")]
        public void ConfirmationUserEmail_UserIdIsValid(string userId)
        {
            var result = Validator.Validate(new ConfirmUserEmailCommand() {Token = "1", UserId = userId});
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ConfirmationUserEmail_UserIdIsInvalid(string userId)
        {
            var result = Validator.Validate(new ConfirmUserEmailCommand() { Token = "1", UserId = userId });
            Assert.Contains("User ID required", result.Errors.First().ErrorMessage);
            Assert.False(result.IsValid);
        }

        [Theory]
        [InlineData("1234567890")]
        [InlineData("0")]
        public void ConfirmationUserEmail_TokenIsValid(string token)
        {
            var result = Validator.Validate(new ConfirmUserEmailCommand() {Token = token, UserId = "1"});
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ConfirmationUserEmail_TokenIsInvalid(string token)
        {
            var result = Validator.Validate(new ConfirmUserEmailCommand() { Token = token, UserId = "1" });
            Assert.Contains("Token required", result.Errors.First().ErrorMessage);
            Assert.False(result.IsValid);
        }
    }
}