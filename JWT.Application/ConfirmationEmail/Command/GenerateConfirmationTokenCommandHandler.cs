using System.Threading;
using System.Threading.Tasks;
using System.Web;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace JWT.Application.ConfirmationEmail.Command
{
    public class GenerateConfirmationTokenCommandHandler : IRequestHandler<GenerateConfirmationTokenCommand, string>
    {
        private readonly UserManager<IdentityUser> _userManager;

        public GenerateConfirmationTokenCommandHandler(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<string> Handle(GenerateConfirmationTokenCommand request, CancellationToken cancellationToken)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(request.User);
            token = HttpUtility.UrlEncode(token);
            return await Task.FromResult(token);
        }
    }
}