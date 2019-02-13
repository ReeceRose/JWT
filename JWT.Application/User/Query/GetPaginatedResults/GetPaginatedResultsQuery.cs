using System.Collections.Generic;
using JWT.Application.User.Model;
using JWT.Domain.Entities;
using MediatR;

namespace JWT.Application.User.Query.GetPaginatedResults
{
    public class GetPaginatedResultsQuery : IRequest<PaginatedUsersDto>
    {
        public GetPaginatedResultsQuery(List<ApplicationUserDto> users, PaginationModel paginationModel)
        {
            Users = users;
            PaginationModel = paginationModel;
        }

        public List<ApplicationUserDto> Users { get; set; }
        public PaginationModel PaginationModel { get; set; }
    }
}
