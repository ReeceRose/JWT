using MediatR;

namespace JWT.Application.User.Command.ResetPassword
{
    public class ResetPasswordCommand : IRequest<bool>
    {
        public ResetPasswordCommand(string token, string email, string password)
        {
            Token = token;
            Email = email;
            Password = password;
        }

        public string Token { get; }
        public string Email { get; }
        public string Password { get; }
    }
}