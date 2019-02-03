using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JWT.Application.User.Model;
using JWT.Application.User.Query.GetAllUsers;
using JWT.Application.User.Query.GetAllUsersPaginated;
using JWT.Application.User.Query.GetPaginatedResults;
using JWT.Application.Utilities;
using JWT.Domain.Entities;
using MediatR;
using Moq;
using Xunit;

namespace JWT.Tests.Core.Application.User.Query.GetAllUsersPaginated
{
    public class GetAllUsersPaginatedTest
    {
        public Mock<IMediator> Mediator { get; }
        public IMapper Mapper { get; }
        public GetAllUsersPaginatedQueryHandler Handler { get; }

        public List<ApplicationUser> Users { get; }

        public GetAllUsersPaginatedTest()
        {
            // Arrange
            Mediator = new Mock<IMediator>();
            Mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
                cfg.ValidateInlineMaps = false;
            }));
            Handler = new GetAllUsersPaginatedQueryHandler(Mediator.Object, Mapper);
            Users = new List<ApplicationUser>()
            {
                new ApplicationUser() {Email = "test@test.ca"},
                new ApplicationUser() {Email = "user@domain.com"},
            };
            Mediator.Setup(m => m.Send(It.IsAny<GetAllUsersQuery>(), default(CancellationToken))).ReturnsAsync(Users);
        }

        [Theory]
        // Based off of the user list above
        [InlineData(3, 1, 1)]
        [InlineData(1, 1, 2)]
        [InlineData(10, 1, 1)]
        public async Task GetAllUsersPaginated_ReturnsExpected(int pageSize, int currentPage, int totalPages)
        {
            // Arrange
            var paginatedUsers = new PaginatedUsersDto()
            {
                PaginationModel = new PaginationModel()
                {
                    CurrentPage = currentPage,
                    PageSize = pageSize
                },
                Users = Mapper.Map<List<ApplicationUserDto>>(Users)
            };
            Mediator.Setup(m => m.Send(It.IsAny<GetPaginatedResultsQuery>(), default(CancellationToken))).ReturnsAsync(paginatedUsers);
            // Act
            var result = await Handler.Handle(new GetAllUsersPaginatedQuery(paginatedUsers.PaginationModel), CancellationToken.None);
            // Assert
            Assert.Equal(pageSize, result.PaginationModel.PageSize);
            Assert.Equal(currentPage, result.PaginationModel.CurrentPage);
            Assert.Equal(totalPages, result.PaginationModel.TotalPages);
        }
    }
}
