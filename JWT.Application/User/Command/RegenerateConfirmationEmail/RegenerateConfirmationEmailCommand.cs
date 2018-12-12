using MediatR;

namespace JWT.Application.User.Command.RegenerateConfirmationEmail
{
    public class RegenerateConfirmationEmailCommand : IRequest<bool>
    {
        public RegenerateConfirmationEmailCommand(string email) => Email = email;

        public string Email { get; }
    }
}
