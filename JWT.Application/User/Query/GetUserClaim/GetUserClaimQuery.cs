using System.Collections.Generic;
using System.Security.Claims;
using JWT.Application.User.Model;
using MediatR;

namespace JWT.Application.User.Query.GetUserClaim
{
    public class GetUserClaimQuery : IRequest<List<Claim>>
    {
        public GetUserClaimQuery(ApplicationUserDto user) => User = user;

        public ApplicationUserDto User { get; }
    }
}
