using JWT.Domain.Exceptions;
using Xunit;

namespace JWT.Tests.Core.Domain.Exceptions
{
    public class InvalidExternalProviderTokenTest
    {

        [Fact]
        public void AccountAlreadyExistsException_ReturnsDefaultErrorMessage()
        {
            // Act
            var error = new InvalidExternalProviderToken();
            // Assert
            Assert.Equal("Invalid access token", error.Message);
            Assert.Equal(typeof(InvalidExternalProviderToken), error.GetType());
        }

        [Theory]
        [InlineData("Invalid external access token")]
        [InlineData("Error: Invalid access token")]
        public void AccountAlreadyExistsException_ReturnsProvidedErrorMessage(string errorMessage)
        {
            // Act
            var error = new InvalidExternalProviderToken(errorMessage);
            // Arrange
            Assert.Equal(errorMessage, error.Message);
            Assert.Equal(typeof(InvalidExternalProviderToken), error.GetType());
        }
    }
}
