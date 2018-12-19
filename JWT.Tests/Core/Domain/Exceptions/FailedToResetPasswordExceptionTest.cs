using JWT.Domain.Exceptions;
using Xunit;

namespace JWT.Tests.Core.Domain.Exceptions
{
    public class FailedToResetPasswordExceptionTest
    {
        [Fact]
        public void FailedToResetException_ReturnsDefaultErrorMessage()
        {
            // Act
            var error = new FailedToResetPassword();
            // Assert
            Assert.Equal("Failed to reset password. Try again", error.Message);
            Assert.Equal(typeof(FailedToResetPassword), error.GetType());
        }

        [Theory]
        [InlineData("Failed to reset password")]
        [InlineData("Error: Cannot reset password")]
        public void FailedToResetException_ReturnsProvidedErrorMessage(string errorMessage)
        {
            // Act
            var error = new FailedToResetPassword(errorMessage);
            // Assert
            Assert.Equal(errorMessage, error.Message);
            Assert.Equal(typeof(FailedToResetPassword), error.GetType());
        }
    }
}
