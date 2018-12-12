using System.Threading;
using JWT.Application.ConfirmationEmail.Command;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Moq;
using Xunit;

namespace JWT.Tests.Core.Application.ConfirmationEmail.Command
{
    public class GenerateConfirmationTokenTest
    {

        [Fact]
        public void AlreadyConfirmedAccountConfirmationCodeShouldNotBeGenerated()
        {
            var mediator = new Mock<IMediator>();
            var user = new IdentityUser()
            {
                Email = "testuser@gmail.com",
                UserName = "testuser@gmail.com",
                EmailConfirmed = true
            };
        }

        [Fact]
        public void NewAccountConfirmationCodeShouldBeGenerated()
        {

        }
    }
}
