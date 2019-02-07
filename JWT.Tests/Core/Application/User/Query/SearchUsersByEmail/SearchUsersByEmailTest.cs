using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JWT.Application.User.Model;
using JWT.Application.User.Query.GetAllUsers;
using JWT.Application.User.Query.GetPaginatedResults;
using JWT.Application.User.Query.SearchUsersByEmail;
using JWT.Application.Utilities;
using JWT.Domain.Entities;
using MediatR;
using Moq;
using Xunit;

namespace JWT.Tests.Core.Application.User.Query.SearchUsersByEmail
{
    public class SearchUsersByEmailTest
    {
        public Mock<IMediator> Mediator { get; }
        public IMapper Mapper { get; }
        public SearchUsersByEmailQueryHandler Handler { get; } 

        public SearchUsersByEmailTest()
        {
            // Arrange
            Mediator = new Mock<IMediator>();
            Mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
                cfg.ValidateInlineMaps = false;
            }));
            Handler = new SearchUsersByEmailQueryHandler(Mediator.Object, Mapper);
        }

        [Theory]
        [InlineData("test", 1, 1)]
        [InlineData("user", 15, 2)]
        [InlineData(".com", 101, 11)]
        public async Task SearchUsersByEmail_ReturnsExpected(string email, int userCount, int pageCount)
        {
            // Arrange
            var users = new List<ApplicationUser>();
            for (var i = 0; i < userCount; i++)
            {
                // Create a unique email
                users.Add(new ApplicationUser() { Email =  $"{i}{email}"});
            }

            var mappedUsers = Mapper.Map<List<ApplicationUserDto>>(users);
            var paginationModel = new PaginationModel() { Count = userCount };
            Mediator.Setup(m => m.Send(It.IsAny<GetAllUsersQuery>(), default(CancellationToken))).ReturnsAsync(users);
            Mediator.Setup(m => m.Send(It.IsAny<GetPaginatedResultsQuery>(), default(CancellationToken)))
                                .ReturnsAsync(new PaginatedUsersDto()
                                { PaginationModel = paginationModel, Users = mappedUsers });

            // Act
            var result = await Handler.Handle(new SearchUsersByEmailQuery(email), CancellationToken.None);
            // Assert
            Assert.Equal(pageCount, result.PaginationModel.TotalPages);
            Assert.Equal(userCount, result.PaginationModel.Count);
            Assert.Equal(users.Count, result.Users.Count);
        }
    }
}
