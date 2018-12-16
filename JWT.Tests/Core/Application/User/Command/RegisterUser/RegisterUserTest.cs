using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JWT.Application.ConfirmationEmail.Command;
using JWT.Application.Interfaces;
using JWT.Application.User.Command.RegisterUser;
using JWT.Application.User.Query.GetUserByEmail;
using JWT.Common;
using JWT.Domain.Exceptions;
using JWT.Tests.Helpers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace JWT.Tests.Core.Application.User.Command.RegisterUser
{
    public class RegisterUserTest
    {
        public Mock<IMediator> Mediator { get; }
        public MapperConfiguration MapperConfiguration { get; }
        public IMapper Mapper { get; }
        public Mock<MockUserManager> UserManager { get; }
        public Mock<INotificationService> NotificationService { get; }
        public Mock<IConfiguration> Configuration { get; }
        public RegisterUserCommandHandler Handler { get; }

        public RegisterUserTest()
        {
            // Arrange
            Mediator = new Mock<IMediator>();
            MapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            Mapper = MapperConfiguration.CreateMapper();
            UserManager = new Mock<MockUserManager>();
            NotificationService = new Mock<INotificationService>();
            Configuration = new Mock<IConfiguration>();
            Handler = new RegisterUserCommandHandler(Mediator.Object, Mapper, UserManager.Object, NotificationService.Object, Configuration.Object);
        }

        [Theory]
        [InlineData("test@test.ca", "Test123!")]
        [InlineData("user@domain.com", "Password!1f4")]
        public void RegisterUser_SuccessfullyRegistersUser(string email, string password)
        {
            // Arrange
            Mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), default(CancellationToken))).ReturnsAsync((IdentityUser) null);
            NotificationService.Setup(n => n.SendNotificationAsync("test", email, "test email", "message")).ReturnsAsync(true);
            UserManager.Setup(u => u.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            Mediator.Setup(m => m.Send(It.IsAny<GenerateConfirmationTokenCommand>(), default(CancellationToken))).ReturnsAsync(It.IsAny<string>());
            // Act
            var result = Handler.Handle(new RegisterUserCommand(email, password, false), CancellationToken.None).Result;
            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData("test@test.ca", "Test123!")]
        [InlineData("user@domain.com", "Password!1f4")]
        public async Task RegisterUser_ThrowsAccountAlreadyExistsException(string email, string password)
        {
            // Arrange
            var requestedUser = new IdentityUser() { Email = email };
            Mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), default(CancellationToken))).ReturnsAsync(requestedUser);
            // Act / Assert
            await Assert.ThrowsAsync<AccountAlreadyExistsException>(() => Handler.Handle(new RegisterUserCommand(email, password, false), CancellationToken.None));
        }

        [Theory]
        [InlineData("test@test.ca", "Test123!")]
        [InlineData("user@domain.com", "Password!1f4")]
        public async Task RegisterUser_FailedToRegistersUser(string email, string password)
        {
            // Arrange
            Mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), default(CancellationToken))).ReturnsAsync((IdentityUser)null);
            UserManager.Setup(u => u.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Failed());
            // Act / Assert
            await Assert.ThrowsAsync<InvalidRegisterException>(() => Handler.Handle(new RegisterUserCommand(email, password, false), CancellationToken.None));
        }
    }
}