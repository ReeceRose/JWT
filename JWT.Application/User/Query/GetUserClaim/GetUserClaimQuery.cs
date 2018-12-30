using System.Collections.Generic;
using System.Security.Claims;
using JWT.Domain.Entities;
using MediatR;

namespace JWT.Application.User.Query.GetUserClaim
{
    public class GetUserClaimQuery : IRequest<List<Claim>>
    {
        public GetUserClaimQuery(ApplicationUser user) => User = user;

        public ApplicationUser User { get; }
    }
}
