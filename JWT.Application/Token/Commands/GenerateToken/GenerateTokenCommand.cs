using System.Collections.Generic;
using System.Security.Claims;
using MediatR;

namespace JWT.Application.Token.Commands.GenerateToken
{
    public class GenerateTokenCommand : IRequest<string>
    {

        public List<Claim> Claims { get; set; }
    }
}
