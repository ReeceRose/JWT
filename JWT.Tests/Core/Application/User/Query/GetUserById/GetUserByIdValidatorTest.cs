using JWT.Application.User.Query.GetUserById;
using Xunit;

namespace JWT.Tests.Core.Application.User.Query.GetUserById
{
    public class GetUserByIdValidatorTest
    {
        public GetUserByIdQueryValidator Validator { get; }

        public GetUserByIdValidatorTest()
        {
            // Arrange
            Validator = new GetUserByIdQueryValidator();
        }

        [Theory]
        [InlineData("1234567890")]
        [InlineData("0987654321")]
        public void GetAUserById_UserIdIsValid(string userId)
        {
            // Act
            var result = Validator.Validate(new GetUserByIdQuery(userId));
            // Assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void GetAUserById_UserIdIsInvalid(string userId)
        {
            // Act
            var result = Validator.Validate(new GetUserByIdQuery(userId));
            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("User ID required", result.Errors[0].ErrorMessage);
        }
    }
}
