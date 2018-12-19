using MediatR;
using Microsoft.AspNetCore.Identity;

namespace JWT.Application.User.Query.GetUserByEmail
{
    public class GetUserByEmailQuery : IRequest<IdentityUser>
    {
        public GetUserByEmailQuery(string email) => Email = email;
        public string Email { get; }
    }
}