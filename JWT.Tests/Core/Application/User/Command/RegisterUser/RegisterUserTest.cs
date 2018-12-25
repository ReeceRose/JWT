﻿using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JWT.Application.User.Command.CreateUser;
using JWT.Application.User.Command.RegisterUser;
using JWT.Application.User.Query.GenerateEmailConfirmation.Email;
using JWT.Application.User.Query.GetUserByEmail;
using JWT.Common;
using JWT.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace JWT.Tests.Core.Application.User.Command.RegisterUser
{
    public class RegisterUserTest
    {
        public Mock<IMediator> Mediator { get; }
        public MapperConfiguration MapperConfiguration { get; }
        public IMapper Mapper { get; }
        public RegisterUserCommandHandler Handler { get; }

        public RegisterUserTest()
        {
            // Arrange
            Mediator = new Mock<IMediator>();
            MapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            Mapper = MapperConfiguration.CreateMapper();
            Handler = new RegisterUserCommandHandler(Mediator.Object, Mapper);
        }

        [Theory]
        [InlineData("test@test.ca", "Test123!")]
        [InlineData("user@domain.com", "Password!1f4")]
        public void RegisterUser_SuccessfullyRegistersUser(string email, string password)
        {
            // Arrange
            Mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), default(CancellationToken))).ReturnsAsync((IdentityUser) null);
            Mediator.Setup(m => m.Send(It.IsAny<GenerateEmailConfirmationEmailQuery>(), default(CancellationToken))).ReturnsAsync(It.IsAny<string>());
            Mediator.Setup(m => m.Send(It.IsAny<CreateUserCommand>(), default(CancellationToken))).ReturnsAsync(IdentityResult.Success);
            Mediator.Setup(m => m.Send(It.IsAny<GenerateEmailConfirmationEmailQuery>(), default(CancellationToken))).ReturnsAsync("123");
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
            Mediator.Setup(m => m.Send(It.IsAny<CreateUserCommand>(), default(CancellationToken))).ReturnsAsync(IdentityResult.Failed());
            // Act / Assert
            await Assert.ThrowsAsync<InvalidRegisterException>(() => Handler.Handle(new RegisterUserCommand(email, password, false), CancellationToken.None));
        }
    }
}