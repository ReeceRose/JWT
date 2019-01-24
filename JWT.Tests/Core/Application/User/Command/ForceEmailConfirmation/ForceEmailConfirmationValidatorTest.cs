using System;
using System.Collections.Generic;
using System.Text;
using JWT.Application.User.Command.EnableUser;
using JWT.Application.User.Command.ForceEmailConfirmation;
using Xunit;

namespace JWT.Tests.Core.Application.User.Command.ForceEmailConfirmation
{
    public class ForceEmailConfirmationValidatorTest
    {
        public ForceEmailConfirmationCommandValidator Validator { get; }

        public ForceEmailConfirmationValidatorTest()
        {
            // Arrange
            Validator = new ForceEmailConfirmationCommandValidator();
        }

        [Theory]
        [InlineData("1234567890")]
        [InlineData("0987654321")]
        public void ForceEmailConfirmation_UserIdIsValid(string userId)
        {
            // Act
            var result = Validator.Validate(new ForceEmailConfirmationCommand(userId));
            // Assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ForceEmailConfirmation_UserIdIsInvalid(string userId)
        {
            // Act
            var result = Validator.Validate(new ForceEmailConfirmationCommand(userId));
            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("User ID required", result.Errors[0].ErrorMessage);
        }
    }
}
