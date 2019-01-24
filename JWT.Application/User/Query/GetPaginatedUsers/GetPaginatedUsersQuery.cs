using JWT.Application.User.Model;
using JWT.Domain.Entities;
using MediatR;

namespace JWT.Application.User.Query.GetPaginatedUsers
{
    public class GetPaginatedUsersQuery : IRequest<GetPaginatedUsersDto>
    {
        public GetPaginatedUsersQuery(PaginationModel model) => PaginationModel = model;

        public PaginationModel PaginationModel { get; set; }
    }
}
