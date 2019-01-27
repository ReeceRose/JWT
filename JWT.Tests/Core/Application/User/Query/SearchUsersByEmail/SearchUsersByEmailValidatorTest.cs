using JWT.Application.User.Query.GetUserByEmail;
using JWT.Application.User.Query.SearchUsersByEmail;
using Xunit;

namespace JWT.Tests.Core.Application.User.Query.SearchUsersByEmail
{
    public class SearchUsersByEmailValidatorTest
    {
        public SearchUsersByEmailQueryValidator Validator { get; }

        public SearchUsersByEmailValidatorTest()
        {
            // Arrange
            Validator = new SearchUsersByEmailQueryValidator();
        }

        [Theory]
        [InlineData("test@test.ca")]
        [InlineData("user@domain.com")]
        public void SearchUsersByEmail_UserIdIsValid(string email)
        {
            // Act
            var result = Validator.Validate(new SearchUsersByEmailQuery(email));
            // Assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void SearchUsersByEmail_UserIdIsInvalid(string email)
        {
            // Act
            var result = Validator.Validate(new SearchUsersByEmailQuery(email));
            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("Email is required", result.Errors[0].ErrorMessage);
        }
    }
}
