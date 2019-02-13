using System.Collections.Generic;
using JWT.Domain.Entities;
using MediatR;

namespace JWT.Application.User.Query.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<List<ApplicationUser>>
    {

    }
}
