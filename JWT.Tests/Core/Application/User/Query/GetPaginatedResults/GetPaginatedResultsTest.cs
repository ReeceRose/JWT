using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JWT.Application.User.Model;
using JWT.Application.User.Query.GetPaginatedResults;
using JWT.Domain.Entities;
using Xunit;

namespace JWT.Tests.Core.Application.User.Query.GetPaginatedResults
{
    public class GetPaginatedResultsTest
    {
        public List<ApplicationUserDto> Users { get; }
        public GetPaginatedResultsQueryHandler Handler { get; }

        public GetPaginatedResultsTest()
        {
            // Arrange
            Users = new List<ApplicationUserDto>()
            {
                new ApplicationUserDto() { Email = "test@test.ca", Id = "123", UserName = "test@test.ca" },
                new ApplicationUserDto() { Email = "user@domain.com", Id = "1234", UserName = "user@domain.com" }
            };
            Handler = new GetPaginatedResultsQueryHandler();
        }

        [Theory]
        // Based off of the user list above
        [InlineData(3, 1)]
        [InlineData(1, 1)]
        [InlineData(10, 1)]
        public async Task GetPaginatedResults_ReturnsExpected(int pageSize, int currentPage)
        {
            // Arrange
            var model = new PaginationModel()
            {
                PageSize = pageSize,
                CurrentPage = currentPage
            };
            // Act
            var result = await Handler.Handle(new GetPaginatedResultsQuery(Users, model), CancellationToken.None);
            // Assert
            Assert.Equal(2, result.Users.Count);
            Assert.Equal(pageSize, result.PaginationModel.PageSize);
            Assert.Equal(currentPage, result.PaginationModel.CurrentPage);
        }
    }
}
