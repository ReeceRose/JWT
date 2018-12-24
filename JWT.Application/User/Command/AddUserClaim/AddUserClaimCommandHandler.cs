using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace JWT.Application.User.Command.AddUserClaim
{
    public class AddUserClaimCommandHandler : IRequestHandler<AddUserClaimCommand, bool>
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AddUserClaimCommandHandler(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Handle(AddUserClaimCommand request, CancellationToken cancellationToken)
        {
            var result = await _userManager.AddClaimAsync(request.User, new Claim(request.Key, request.Value));
            return result.Succeeded;
        }
    }
}
