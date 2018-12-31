using JWT.Application.User.Model;
using JWT.Domain.Entities;
using MediatR;

namespace JWT.Application.User.Query.GetUserByEmail
{
    public class GetUserByEmailQuery : IRequest<ApplicationUserDto>
    {
        public GetUserByEmailQuery(string email) => Email = email;
        public string Email { get; }
    }
}