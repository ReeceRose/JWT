using JWT.Domain.Exceptions;
using Xunit;

namespace JWT.Tests.Core.Domain.Exceptions
{
    public class EmailIsAlreadyConfirmedExceptionTest
    {
        [Fact]
        public void EmailIsAlreadyConfirmedException_ReturnsDefaultErrorMessage()
        {
            // Act
            var error = new EmailIsAlreadyConfirmedException();
            // Assert
            Assert.Equal("Email already confirmed", error.Message);
            Assert.Equal(typeof(EmailIsAlreadyConfirmedException), error.GetType());
        }

        [Theory]
        [InlineData("This account is locked")]
        [InlineData("Error: Account locked")]
        public void EmailIsAlreadyConfirmedException_ReturnsProvidedErrorMessage(string errorMessage)
        {
            // Act
            var error = new EmailIsAlreadyConfirmedException(errorMessage);
            // Assert
            Assert.Equal(errorMessage, error.Message);
            Assert.Equal(typeof(EmailIsAlreadyConfirmedException), error.GetType());
        }
    }
}
