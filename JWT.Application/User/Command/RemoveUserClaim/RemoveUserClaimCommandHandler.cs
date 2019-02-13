using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using JWT.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace JWT.Application.User.Command.RemoveUserClaim
{
    public class RemoveUserClaimCommandHandler : IRequestHandler<RemoveUserClaimCommand, bool>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RemoveUserClaimCommandHandler> _logger;

        public RemoveUserClaimCommandHandler(UserManager<ApplicationUser> userManager, ILogger<RemoveUserClaimCommandHandler> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }
        
        public async Task<bool> Handle(RemoveUserClaimCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Remove Claim: {request.User.Email}: Removing claim ({request.Key})");
            var result = await _userManager.RemoveClaimAsync(request.User, new Claim(request.Key, ""));
            return result.Succeeded;
        }
    }
}
