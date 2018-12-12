using System.Collections.Generic;
using System.Security.Claims;
using MediatR;

namespace JWT.Application.Token.Query.GetToken
{
    public class GenerateTokenQuery : IRequest<string>
    {
        public IList<Claim> Claims { get; }

        public GenerateTokenQuery(IList<Claim> claims) => Claims = claims;
    }
}
