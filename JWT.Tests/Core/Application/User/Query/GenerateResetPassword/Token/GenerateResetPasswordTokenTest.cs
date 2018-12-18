using System.Threading;
using JWT.Application.User.Query.GenerateResetPassword.Token;
using Microsoft.AspNetCore.Identity;
using Xunit;

namespace JWT.Tests.Core.Application.User.Query.GenerateResetPassword.Token
{
    public class GenerateResetPasswordTokenTest
    {
        public GenerateResetPasswordTokenQueryHandler Handler { get; }

        public GenerateResetPasswordTokenTest()
        {
            // Arrange
            Handler = new GenerateResetPasswordTokenQueryHandler();
        }

        [Theory]
        [InlineData("email", "token")]
        public void GenerateResetPasswordToken_ReturnsValidToken(string email, string token)
        {
            // Arrange
            var requestedUser = new IdentityUser()
            {
                Email = email
            };
            // Act
            var returnedToken = Handler.Handle(new GenerateResetPasswordTokenQuery(requestedUser), CancellationToken.None).Result;
            // Assert
            Assert.Equal(returnedToken, token);
        }
    }
}