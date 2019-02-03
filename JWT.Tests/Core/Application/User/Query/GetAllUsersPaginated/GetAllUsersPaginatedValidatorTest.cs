using JWT.Application.User.Query.GetAllUsersPaginated;
using JWT.Domain.Entities;
using Xunit;

namespace JWT.Tests.Core.Application.User.Query.GetAllUsersPaginated
{
    public class GetAllUsersPaginatedValidatorTest
    {
        public GetAllUsersPaginatedQueryValidator Validator { get; }

        public GetAllUsersPaginatedValidatorTest()
        {
            // Arrange
            Validator = new GetAllUsersPaginatedQueryValidator();
        }

        [Theory]
        [InlineData(1, 10)]
        [InlineData(2, 15)]
        [InlineData(3, 100)]
        public void GetAllUsersPaginated_PaginatedModelIdIsValid(int currentPage, int pageSize)
        {
            // Act
            var model = new PaginationModel() {CurrentPage = currentPage, PageSize = pageSize};
            var result = Validator.Validate(new GetAllUsersPaginatedQuery(model));
            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void GetAllUsersPaginated_PaginatedModelIsInvalid()
        {
            // Act
            var result = Validator.Validate(new GetAllUsersPaginatedQuery(null));
            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("Pagination model required", result.Errors[0].ErrorMessage);
        }
    }
}
