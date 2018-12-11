using MediatR;
using Microsoft.AspNetCore.Identity;

namespace JWT.Application.ConfirmationEmail.Command
{
    public class GenerateConfirmationTokenCommand : IRequest<string>
    {
        public GenerateConfirmationTokenCommand(IdentityUser user) => User = user;

        public IdentityUser User{ get; }
    }
}
