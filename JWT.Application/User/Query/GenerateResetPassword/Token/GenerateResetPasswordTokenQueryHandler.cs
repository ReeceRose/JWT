using System.Threading;
using System.Threading.Tasks;
using System.Web;
using JWT.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace JWT.Application.User.Query.GenerateResetPassword.Token
{
    public class GenerateResetPasswordTokenQueryHandler : IRequestHandler<GenerateResetPasswordTokenQuery, string>
    {

        private readonly UserManager<ApplicationUser> _userManager;

        public GenerateResetPasswordTokenQueryHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> Handle(GenerateResetPasswordTokenQuery request, CancellationToken cancellationToken)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(request.User);
            token = HttpUtility.UrlEncode(token);
            return await Task.FromResult(token);
        }
    }
}