using JWT.Application.Users.Models;
using MediatR;

namespace JWT.Application.Users.Queries
{
    public class GetUserByEmailQuery : IRequest<ApplicationUserDto>
    {
        public string Email { get; }
        public GetUserByEmailQuery(string email) => Email = email;
    }
}
