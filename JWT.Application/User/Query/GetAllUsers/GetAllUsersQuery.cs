using System.Collections.Generic;
using JWT.Application.User.Model;
using MediatR;

namespace JWT.Application.User.Query.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<List<ApplicationUserDto>>
    {

    }
}
