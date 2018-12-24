using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace JWT.Application.User.Query.GetUserClaim
{
    public class GetUserClaimQueryHandler : IRequestHandler<GetUserClaimQuery, List<Claim>>
    {
        public Task<List<Claim>> Handle(GetUserClaimQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
