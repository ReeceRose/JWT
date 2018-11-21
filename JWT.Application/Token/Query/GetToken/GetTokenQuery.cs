using System.Collections.Generic;
using System.Security.Claims;
using MediatR;

namespace JWT.Application.Token.Query.GetToken
{
    public class GetTokenQuery : IRequest<string>
    {

        public List<Claim> Claims { get; set; }
    }
}
