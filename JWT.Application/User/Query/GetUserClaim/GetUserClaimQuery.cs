using System.Collections.Generic;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace JWT.Application.User.Query.GetUserClaim
{
    public class GetUserClaimQuery : IRequest<List<Claim>>
    {
        public GetUserClaimQuery(IdentityUser user) => User = user;

        public IdentityUser User { get; }
    }
}
