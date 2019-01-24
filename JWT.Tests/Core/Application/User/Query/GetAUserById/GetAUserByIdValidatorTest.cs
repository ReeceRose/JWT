using JWT.Application.User.Query.GetAUserById;
using Xunit;

namespace JWT.Tests.Core.Application.User.Query.GetAUserById
{
    public class GetAUserByIdValidatorTest
    {
        public GetAUserByIdQueryValidator Validator { get; }

        public GetAUserByIdValidatorTest()
        {
            // Arrange
            Validator = new GetAUserByIdQueryValidator();
        }

        [Theory]
        [InlineData("1234567890")]
        [InlineData("0987654321")]
        public void GetAUserById_UserIdIsValid(string userId)
        {
            // Act
            var result = Validator.Validate(new GetAUserByIdQuery(userId));
            // Assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void GetAUserById_UserIdIsInvalid(string userId)
        {
            // Act
            var result = Validator.Validate(new GetAUserByIdQuery(userId));
            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("User ID required", result.Errors[0].ErrorMessage);
        }
    }
}