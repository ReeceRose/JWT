using JWT.Domain.Entities;
using MediatR;

namespace JWT.Application.User.Query.GetUserByEmail
{
    public class GetUserByEmailQuery : IRequest<ApplicationUser>
    {
        public GetUserByEmailQuery(string email) => Email = email;
        public string Email { get; }
    }
}