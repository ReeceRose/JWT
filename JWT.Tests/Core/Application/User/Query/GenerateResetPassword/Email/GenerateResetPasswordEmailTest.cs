using System.Threading;
using JWT.Application.Interfaces;
using JWT.Application.User.Query.GenerateResetPassword.Email;
using JWT.Application.User.Query.GenerateResetPassword.Token;
using JWT.Application.User.Query.GetUserByEmail;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace JWT.Tests.Core.Application.User.Query.GenerateResetPassword.Email
{
    public class GenerateResetPasswordEmailTest
    {
        public Mock<IMediator> Mediator { get; }
        public Mock<INotificationService> NotificationService { get; }
        public Mock<IConfiguration> Configuration { get; }
        public GenerateResetPasswordEmailQueryHandler Handler { get; }

        public GenerateResetPasswordEmailTest()
        {
            // Arrange
            Mediator = new Mock<IMediator>();
            NotificationService = new Mock<INotificationService>();
            Configuration = new Mock<IConfiguration>();
            Configuration.SetupGet(x => x["FrontEndUrl"]).Returns("url.com");
            Handler = new GenerateResetPasswordEmailQueryHandler(Mediator.Object, NotificationService.Object, Configuration.Object);
        }
        // Email sent
        [Theory]
        [InlineData("test@test.ca", "1234567890")]
        [InlineData("user@domain.com", "9876543210")]
        public void GenerateResetPasswordEmail_EmailSent(string email, string token)
        {
            // Arrange
            var requestedUser = new IdentityUser()
            {
                Email = email
            };
            Mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), default(CancellationToken))).ReturnsAsync(requestedUser);
            Mediator.Setup(m => m.Send(It.IsAny<GenerateResetPasswordTokenQuery>(), default(CancellationToken))).ReturnsAsync(token);
            NotificationService.Setup(n => n.SendNotificationAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);
            // Act
            var result = Handler.Handle(new GenerateResetPasswordEmailQuery(email), CancellationToken.None).Result;
            // Assert
            Assert.Equal(token, result);
        }

        // Null on invalid user
        [Theory]
        [InlineData("test@test.ca")]
        [InlineData(null)]
        public void GenerateResetPasswordEmail_ReturnsNullOnInvalidUser(string email)
        {
            // Arrange
            Mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), default(CancellationToken))).ReturnsAsync((IdentityUser) null);
            // Act
            var result = Handler.Handle(new GenerateResetPasswordEmailQuery(email), default(CancellationToken)).Result;
            // Assert
            Assert.Null(result);
        }
    }
}