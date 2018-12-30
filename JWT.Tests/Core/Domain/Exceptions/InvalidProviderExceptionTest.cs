using JWT.Domain.Exceptions;
using Xunit;

namespace JWT.Tests.Core.Domain.Exceptions
{
    public class InvalidProviderExceptionTest
    {
        [Fact]
        public void InvalidProviderException_ReturnsDefaultErrorMessage()
        {
            // Act
            var error = new InvalidProviderException();
            // Assert
            Assert.Equal("Invalid provider", error.Message);
            Assert.Equal(typeof(InvalidProviderException), error.GetType());
        }

        [Theory]
        [InlineData("This provider is not supported")]
        [InlineData("Error: Invalid provider")]
        public void InvalidProviderException_ReturnsProvidedErrorMessage(string errorMessage)
        {
            // Act
            var error = new InvalidProviderException(errorMessage);
            // Arrange
            Assert.Equal(errorMessage, error.Message);
            Assert.Equal(typeof(InvalidProviderException), error.GetType());
        }
    }
}
