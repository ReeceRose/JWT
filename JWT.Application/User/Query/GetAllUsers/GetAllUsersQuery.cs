using System.Collections.Generic;
using JWT.Application.User.Model;
using JWT.Domain.Entities;
using MediatR;

namespace JWT.Application.User.Query.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<List<ApplicationUserDto>>
    {
        public GetAllUsersQuery(PaginationModel model) => PaginationModel = model;
        public PaginationModel PaginationModel { get; }
    }
}
