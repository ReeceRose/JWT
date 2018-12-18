using System.Threading;
using System.Threading.Tasks;
using JWT.Application.Interfaces;
using JWT.Application.User.Query.GenerateEmailConfirmation.Email;
using JWT.Application.User.Query.GenerateEmailConfirmation.Token;
using JWT.Application.User.Query.GetUserByEmail;
using JWT.Tests.Helpers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace JWT.Tests.Core.Application.User.Query.GenerateEmailConfirmation.Email
{
    public class GenerateEmailConfirmationEmailTest
    {
        public Mock<IMediator> Mediator { get; }
        public Mock<INotificationService> NotificationService { get; }
        public Mock<IConfiguration> Configuration { get; }
        public Mock<MockUserManager> UserManager { get; }
        public GenerateEmailConfirmationEmailQueryHandler Handler { get; }

        public GenerateEmailConfirmationEmailTest()
        {
            // Arrange
            Mediator = new Mock<IMediator>();
            NotificationService = new Mock<INotificationService>();
            Configuration = new Mock<IConfiguration>();
            Configuration.SetupGet(x => x["FrontEndUrl"]).Returns("url.com");
            UserManager = new Mock<MockUserManager>();
            Handler = new GenerateEmailConfirmationEmailQueryHandler(Mediator.Object, NotificationService.Object, Configuration.Object, UserManager.Object);
        }

        [Theory]
        [InlineData("test@test.ca", "123")]
        [InlineData("user@domain.com" ,"321")]
        public void GenerateEmailConfirmationEmail_NewNotificationSent(string email, string token)
        {
            // Arrange
            var requestedUser = new IdentityUser()
            {
                Email = email,
                EmailConfirmed = false
            };
            Mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), default(CancellationToken))).ReturnsAsync(requestedUser);
            NotificationService.Setup(n => n.SendNotificationAsync("test", email, "test email", "message")).ReturnsAsync(true);
            UserManager.Setup(u => u.IsEmailConfirmedAsync(It.IsAny<IdentityUser>())).ReturnsAsync(false);
            Mediator.Setup(m => m.Send(It.IsAny<GenerateEmailConfirmationTokenQuery>(), default(CancellationToken))).ReturnsAsync(token);
            // Act
            var returnedToken = Handler.Handle(new GenerateEmailConfirmationEmailQuery(email), CancellationToken.None);
            // Assert
            Assert.Contains(token, returnedToken.Result);
        }

        [Theory]
        [InlineData("test@test.ca")]
        [InlineData("user@domain.com")]
        public void GenerateEmailConfirmationEmail_ReturnsNullOnInvalidUser(string email)
        {
            // Arrange
            Mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), default(CancellationToken))).ReturnsAsync((IdentityUser) null);
            // Act
            var returnedToken = Handler.Handle(new GenerateEmailConfirmationEmailQuery(email), CancellationToken.None).Result;
            // Assert
            Assert.Null(returnedToken);
        }

        [Theory]
        [InlineData("test@test.ca")]
        [InlineData("user@domain.com")]
        public void GenerateEmailConfirmationEmail_ReturnsNullOnEmailAlreadyConfirmed(string email)
        {
            // Arrange
            Mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), default(CancellationToken))).Returns(Task.FromResult((IdentityUser)null));
            UserManager.Setup(u => u.IsEmailConfirmedAsync(It.IsAny<IdentityUser>())).ReturnsAsync(true);
            // Act
            var returnedToken = Handler.Handle(new GenerateEmailConfirmationEmailQuery(email), CancellationToken.None).Result;
            // Assert
            Assert.Null(returnedToken);
        }
    }
}