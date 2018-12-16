using JWT.Domain.Exceptions;
using Xunit;

namespace JWT.Tests.Core.Domain.Exceptions
{
    public class AccountLockedExceptionTest
    {
        [Fact]
        public void AccountLockedException_ReturnsDefaultErrorMessage()
        {
            // Act
            var error = new AccountLockedException();
            // Assert
            Assert.Equal("Your account is locked", error.Message);
            Assert.Equal(typeof(AccountLockedException), error.GetType());
        }

        [Theory]
        [InlineData("This account is locked")]
        [InlineData("Error: Account locked")]
        public void AccountLockedException_ReturnsProvidedErrorMessage(string errorMessage)
        {
            // Act
            var error = new AccountLockedException(errorMessage);
            // Assert
            Assert.Equal(errorMessage, error.Message);
            Assert.Equal(typeof(AccountLockedException), error.GetType());
        }
    }
}
