using MediatR;

namespace JWT.Application.Users.Queries.LoginUser
{
    public class LoginUserQuery : IRequest<string>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}