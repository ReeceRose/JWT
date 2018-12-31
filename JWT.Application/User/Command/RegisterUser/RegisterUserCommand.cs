using MediatR;

namespace JWT.Application.User.Command.RegisterUser
{
    public class RegisterUserCommand : IRequest<bool>
    {
        public RegisterUserCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; }
        public string Password { get; }
    }
}
