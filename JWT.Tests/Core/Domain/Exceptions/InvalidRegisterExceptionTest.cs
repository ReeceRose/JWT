using JWT.Domain.Exceptions;
using Xunit;

namespace JWT.Tests.Core.Domain.Exceptions
{
    public class InvalidRegisterExceptionTest
    {
        [Fact]
        public void InvalidRegisterException_ReturnsDefaultErrorMessage()
        {
            // Act
            var error = new InvalidRegisterException();
            // Assert
            Assert.Equal("Failed to create account. Please try again", error.Message);
            Assert.Equal(typeof(InvalidRegisterException), error.GetType());
        }

        [Theory]
        [InlineData("Failed to create account")]
        [InlineData("Error: Account creation failed")]
        public void InvalidRegisterException_ReturnsProvidedErrorMessage(string errorMessage)
        {
            // Act
            var error = new InvalidRegisterException(errorMessage);
            // Assert
            Assert.Equal(errorMessage, error.Message);
            Assert.Equal(typeof(InvalidRegisterException), error.GetType());
        }
    }
}
