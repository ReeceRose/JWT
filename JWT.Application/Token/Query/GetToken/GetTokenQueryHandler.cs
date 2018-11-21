using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace JWT.Application.Token.Query.GetToken
{
    public class GetTokenQueryHandler :IRequestHandler<GetTokenQuery, string>
    {
        public Task<string> Handle(GetTokenQuery request, CancellationToken cancellationToken)
        {
            //var securityKey = GET CONFIG SECURITY KEY
            var securityKey = "PLACE YOUR KEY HERE";

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
            
            var token = new JwtSecurityToken(
                issuer: "Issuer",
                audience: "Audience",
                claims: request.Claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: signingCredentials
            );
            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}