using JWT.Application.User.Model;
using JWT.Domain.Entities;
using MediatR;

namespace JWT.Application.User.Query.GetAllUsersPaginated
{
    public class GetAllUsersPaginatedQuery : IRequest<PaginatedUsersDto>
    {
        public GetAllUsersPaginatedQuery(PaginationModel model) => PaginationModel = model;

        public PaginationModel PaginationModel { get; set; }
    }
}
