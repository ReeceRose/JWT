using System.Threading;
using System.Threading.Tasks;
using JWT.Application.User.Model;
using MediatR;

namespace JWT.Application.User.Query.GetPaginatedResults
{
    public class GetPaginatedResultsQueryHandler : IRequestHandler<GetPaginatedResultsQuery, PaginatedUsersDto>
    {
        public Task<PaginatedUsersDto> Handle(GetPaginatedResultsQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new PaginatedUsersDto()
            {
                PaginationModel = request.PaginationModel,
                Users = request.Users
            });
        }
    }
}
