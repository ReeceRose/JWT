using MediatR;

namespace JWT.Application.Users.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<string>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}
