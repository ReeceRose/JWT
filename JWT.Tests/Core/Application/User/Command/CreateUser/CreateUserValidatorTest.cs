using System.Linq;
using JWT.Application.User.Command.CreateUser;
using Microsoft.AspNetCore.Identity;
using Xunit;

namespace JWT.Tests.Core.Application.User.Command.CreateUser
{
    public class CreateUserValidatorTest
    {
        public CreateUserCommandValidator Validator { get; }

        public CreateUserValidatorTest()
        {
            // Arrange
            Validator = new CreateUserCommandValidator();
        }

        [Theory]
        [InlineData("test-user", "Test1!")]
        [InlineData("user@domain.com", "Password1!")]
        public void CreateUser_UserIsValid(string userName, string password)
        {
            // Arrange
            var user = new ApplicationUser()
            {
                UserName = userName,
                Id = "123"
            };
            // Act
            var result = Validator.Validate(new CreateUserCommand(user, password));
            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void CreateUser_UserIsInvalid()
        {
            // Act
            var result = Validator.Validate(new CreateUserCommand(null, "Test1!"));
            // Assert
            Assert.Contains("User required", result.Errors.First().ErrorMessage);
            Assert.False(result.IsValid);
        }

        [Theory]
        [InlineData("Test1!")]
        [InlineData("Password!1")]
        public void CreateUser_PasswordIsValid(string password)
        {
            // Arrange
            var user = new ApplicationUser()
            {
                Email = "test@test.com"
            };
            // Act
            var result = Validator.Validate(new CreateUserCommand(user, password));
            // Assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void CreateUser_PasswordIsInvalid(string password)
        {
            // Arrange
            var user = new ApplicationUser()
            {
                Email = "test@test.com"
            };
            // Act
            var result = Validator.Validate(new CreateUserCommand(user, password));
            // Assert
            Assert.Contains("Password required", result.Errors.First().ErrorMessage);
            Assert.False(result.IsValid);
        }
    }
}
