using JWT.Application.User.Query.GetUserByEmail;
using Xunit;

namespace JWT.Tests.Core.Application.User.Query.GetUserByEmail
{
    public class GetUserByEmailValidatorTest
    {
        public GetUserByEmailQueryValidator Validator { get; }

        public GetUserByEmailValidatorTest()
        {
            // Arrange
            Validator = new GetUserByEmailQueryValidator();
        }

        [Theory]
        [InlineData("test@test.ca")]
        [InlineData("user@domain.com")]
        public void GetUserByEmail_UserIdIsValid(string email)
        {
            // Act
            var result = Validator.Validate(new GetUserByEmailQuery(email));
            // Assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void GetUserByEmail_UserIdIsInvalid(string email)
        {
            // Act
            var result = Validator.Validate(new GetUserByEmailQuery(email));
            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("Email is required", result.Errors[0].ErrorMessage);
        }
    }
}
