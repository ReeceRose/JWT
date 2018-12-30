using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace JWT.Application.User.Query.GetUserClaim
{
    public class GetUserClaimQueryHandler : IRequestHandler<GetUserClaimQuery, List<Claim>>
    {
        private readonly UserManager<IdentityUser> _userManager;

        public GetUserClaimQueryHandler(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<Claim>> Handle(GetUserClaimQuery request, CancellationToken cancellationToken)
        {
            var claims = await _userManager.GetClaimsAsync(request.User);
            return claims.ToList();
        }
    }
}
