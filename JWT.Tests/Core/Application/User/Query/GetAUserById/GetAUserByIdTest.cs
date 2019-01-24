using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JWT.Application.User.Query.GetAUserById;
using JWT.Application.User.Query.GetUserById;
using JWT.Application.Utilities;
using JWT.Domain.Entities;
using MediatR;
using Moq;
using Xunit;

namespace JWT.Tests.Core.Application.User.Query.GetAUserById
{
    public class GetAUserByIdTest
    {
        public Mock<IMediator> Mediator { get; }
        public IMapper Mapper { get; }
        public GetAUserByIdQueryHandler Handler { get; }   

        public GetAUserByIdTest()
        {
            // Arrange
            Mediator = new Mock<IMediator>();
            Mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
                cfg.ValidateInlineMaps = false;
            }));
            Handler = new GetAUserByIdQueryHandler(Mediator.Object, Mapper);
        }

        [Theory]
        [InlineData("1234567890")]
        [InlineData("0987654321")]
        public async Task GetAUserById_ReturnsNull(string userId)
        {
            // Arrange
            Mediator.Setup(m => m.Send(It.IsAny<GetUserByIdQuery>(), default(CancellationToken))).ReturnsAsync((ApplicationUser) null);
            // Act
            var result = await Handler.Handle(new GetAUserByIdQuery(userId), CancellationToken.None);
            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData("1234567890")]
        [InlineData("0987654321")]
        public async Task GetAUserById_ReturnsValidUser(string userId)
        {
            var user = new ApplicationUser()
            {
                Id = userId,
                Email = "test@test.ca"
            };
            // Arrange
            Mediator.Setup(m => m.Send(It.IsAny<GetUserByIdQuery>(), default(CancellationToken))).ReturnsAsync(user);
            // Act
            var result = await Handler.Handle(new GetAUserByIdQuery(userId), CancellationToken.None);
            // Assert
            Assert.Equal(userId, result.Id);
            Assert.Equal(user.Email, result.Email);
        }
    }
}
