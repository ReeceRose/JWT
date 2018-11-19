using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace JWT.Application.Token.Commands.GenerateToken
{
    public class GenerateTokenCommandHandler :IRequestHandler<GenerateTokenCommand, string>
    {
        public Task<string> Handle(GenerateTokenCommand request, CancellationToken cancellationToken)
        {
            //var securityKey = GET CONFIG SECURITY KEY
            var securityKey = "PLACE YOUR KEY HERE";

            var symmetricSecutiyKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

            var signingCredentials = new SigningCredentials(symmetricSecutiyKey, SecurityAlgorithms.HmacSha256Signature);
            
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