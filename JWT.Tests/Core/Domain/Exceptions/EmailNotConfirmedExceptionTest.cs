using JWT.Domain.Exceptions;
using Xunit;

namespace JWT.Tests.Core.Domain.Exceptions
{
    public class EmailNotConfirmedExceptionTest
    {
        [Fact]
        public void EmailNotConfirmedException_ReturnsDefaultErrorMessage()
        {
            // Act
            var error = new EmailNotConfirmedException();
            // Assert
            Assert.Equal("Email not confirmed", error.Message);
            Assert.Equal(typeof(EmailNotConfirmedException), error.GetType());
        }

        [Theory]
        [InlineData("Please confirm your email")]
        [InlineData("Error: Email not confirmed")]
        public void EmailNotConfirmedException_ReturnsProvidedErrorMessage(string errorMessage)
        {
            // Act
            var error = new EmailNotConfirmedException(errorMessage);
            // Assert
            Assert.Equal(errorMessage, error.Message);
            Assert.Equal(typeof(EmailNotConfirmedException), error.GetType());
        }
    }
}
