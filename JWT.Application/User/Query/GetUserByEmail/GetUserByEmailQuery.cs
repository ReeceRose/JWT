using MediatR;
using Microsoft.AspNetCore.Identity;

namespace JWT.Application.User.Query.GetUserByEmail
{
    public class GetUserByEmailQuery : IRequest<IdentityUser>
    {
        public string Email { get; }
        public GetUserByEmailQuery(string email) => Email = email;
    }
}
