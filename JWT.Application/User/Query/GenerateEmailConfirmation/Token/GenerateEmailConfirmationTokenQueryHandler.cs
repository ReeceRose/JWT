using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JWT.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace JWT.Application.User.Query.GenerateEmailConfirmation.Token
{
    public class GenerateEmailConfirmationTokenQueryHandler : IRequestHandler<GenerateEmailConfirmationTokenQuery, string>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public GenerateEmailConfirmationTokenQueryHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<string> Handle(GenerateEmailConfirmationTokenQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(await _userManager.GenerateEmailConfirmationTokenAsync(request.User));
        }
    }
}