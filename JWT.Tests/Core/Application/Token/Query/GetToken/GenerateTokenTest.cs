using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using JWT.Application.Token.Query.GetToken;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace JWT.Tests.Core.Application.Token.Query.GetToken
{
    public class GenerateTokenTest
    {
        private readonly Mock<IConfiguration> _configuration;

        private List<Claim> Claims { get; }

        public GenerateTokenTest()
        {
            _configuration = new Mock<IConfiguration>();
            _configuration.SetupGet(x => x["JWT:Issuer"]).Returns("issuer.com");
            _configuration.SetupGet(x => x["JWT:Audience"]).Returns("audience.com");
            _configuration.SetupGet(x => x["JWT:SigningKey"]).Returns("testing signing key");

            Claims = new List<Claim>()
            {
                new Claim("type", "value")
            };

        }
        [Fact]
        public void GenerateTokenQuery_ShouldReturnToken()
        {
            var handler = new GenerateTokenQueryHandler(_configuration.Object);

            var token = handler.Handle(new GenerateTokenQuery(Claims), CancellationToken.None).Result;

            Assert.NotNull(token);
            Assert.NotEmpty(token);
        }

        [Fact]
        public void GenerateTokenQuery_IssuerIsValid()
        {
            var handler = new GenerateTokenQueryHandler(_configuration.Object);

            var token = handler.Handle(new GenerateTokenQuery(Claims), CancellationToken.None).Result;

            Assert.Equal("issuer.com", new JwtSecurityToken(token).Issuer);
        }

        [Fact]
        public void GenerateTokenQuery_AudienceIsValid()
        {
            var handler = new GenerateTokenQueryHandler(_configuration.Object);

            var token = handler.Handle(new GenerateTokenQuery(Claims), CancellationToken.None).Result;

            Assert.Equal("audience.com", new JwtSecurityToken(token).Audiences.First());
        }
    }
}