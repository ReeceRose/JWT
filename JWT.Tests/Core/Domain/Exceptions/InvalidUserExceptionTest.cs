using JWT.Domain.Exceptions;
using Xunit;

namespace JWT.Tests.Core.Domain.Exceptions
{
    public class InvalidUserExceptionTest
    {
        [Fact]
        public void InvalidRegisterException_ReturnsDefaultErrorMessage()
        {
            // Act
            var error = new InvalidUserException();
            // Assert
            Assert.Equal("A user with this ID or email does not exist", error.Message);
            Assert.Equal(typeof(InvalidUserException), error.GetType());
        }

        [Theory]
        [InlineData("Unknown user")]
        [InlineData("Error: Not a user")]
        public void InvalidUserException_ReturnsProvidedErrorMessage(string errorMessage)
        {
            // Act
            var error = new InvalidUserException(errorMessage);
            // Assert
            Assert.Equal(errorMessage, error.Message);
            Assert.Equal(typeof(InvalidUserException), error.GetType());
        }
    }
}
