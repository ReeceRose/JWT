using MediatR;

namespace JWT.Application.Users.Commands.RegenerateConfirmationEmail
{
    public class RegenerateConfirmationEmailCommand : IRequest<bool>
    {
        public RegenerateConfirmationEmailCommand(string email) => Email = email;

        public string Email { get; }
    }
}
