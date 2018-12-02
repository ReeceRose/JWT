using MediatR;

namespace JWT.Application.Users.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<bool>
    {
        public RegisterUserCommand(string email, string password, bool isAdmin)
        {
            Email = email;
            Password = password;
            IsAdmin = isAdmin;
        }

        public string Email { get; }
        public string Password { get; }
        public bool IsAdmin { get; }
    }
}
