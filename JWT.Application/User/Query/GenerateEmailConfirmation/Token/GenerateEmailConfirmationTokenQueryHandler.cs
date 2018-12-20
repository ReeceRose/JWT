using System.Threading;
using System.Threading.Tasks;
using System.Web;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace JWT.Application.User.Query.GenerateEmailConfirmation.Token
{
    public class GenerateEmailConfirmationTokenQueryHandler : IRequestHandler<GenerateEmailConfirmationTokenQuery, string>
    {
        private readonly UserManager<IdentityUser> _userManager;

        public GenerateEmailConfirmationTokenQueryHandler(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<string> Handle(GenerateEmailConfirmationTokenQuery request, CancellationToken cancellationToken)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(request.User);
            token = HttpUtility.UrlEncode(token);
            return await Task.FromResult(token);
        }
    }
}