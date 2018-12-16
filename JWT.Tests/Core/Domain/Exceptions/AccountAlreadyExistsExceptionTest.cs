using JWT.Domain.Exceptions;
using Xunit;

namespace JWT.Tests.Core.Domain.Exceptions
{
    public class AccountAlreadyExistsExceptionTest
    {
        [Fact]
        public void AccountAlreadyExistsException_ReturnsDefaultErrorMessage()
        {
            // Act
            var error = new AccountAlreadyExistsException();
            // Assert
            Assert.Equal("Account with this email already exists", error.Message);
            Assert.Equal(typeof(AccountAlreadyExistsException), error.GetType());
        }

        [Theory]
        [InlineData("This account already exists")]
        [InlineData("Emails must be unique")]
        public void AccountAlreadyExistsException_ReturnsProvidedErrorMessage(string errorMessage)
        {
            // Act
            var error = new AccountAlreadyExistsException(errorMessage);
            // Arrange
            Assert.Equal(errorMessage, error.Message);
            Assert.Equal(typeof(AccountAlreadyExistsException), error.GetType());
        }
    }
}
