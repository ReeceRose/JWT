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
        public Mock<IConfiguration> Configuration;

        private List<Claim> Claims { get; }
        public GenerateTokenQueryHandler Handler { get; }

        public GenerateTokenTest()
        {
            // Arrange
            Configuration = new Mock<IConfiguration>();
            Configuration.SetupGet(x => x["JWT:Issuer"]).Returns("issuer.com");
            Configuration.SetupGet(x => x["JWT:Audience"]).Returns("audience.com");
            Configuration.SetupGet(x => x["JWT:SigningKey"]).Returns("testing signing key");

            Claims = new List<Claim>()
            {
                new Claim("type", "value")
            };
            Handler = new GenerateTokenQueryHandler(Configuration.Object);
        }
        [Fact]
        public void GenerateTokenQuery_ShouldReturnToken()
        {
            // Act
            var token = Handler.Handle(new GenerateTokenQuery(Claims), CancellationToken.None).Result;
            // Assert
            Assert.NotNull(token);
            Assert.NotEmpty(token);
        }

        [Fact]
        public void GenerateTokenQuery_IssuerIsValid()
        {
            // Act
            var token = Handler.Handle(new GenerateTokenQuery(Claims), CancellationToken.None).Result;
            var test = new JwtSecurityToken(token);
            // Assert
            Assert.Equal("issuer.com", new JwtSecurityToken(token).Issuer);
        }

        [Fact]
        public void GenerateTokenQuery_AudienceIsValid()
        {
            // Act
            var token = Handler.Handle(new GenerateTokenQuery(Claims), CancellationToken.None).Result;
            // Assert
            Assert.Equal("audience.com", new JwtSecurityToken(token).Audiences.First());
        }

        [Fact]
        public void GenerateTokenQuery_HasPassedClaims()
        {
            // Act
            var token = Handler.Handle(new GenerateTokenQuery(Claims), CancellationToken.None).Result;
            // Assert
            Assert.Contains(Claims.First().Value, new JwtSecurityToken(token).Claims.First(claim => claim.Value == Claims.First().Value).Value);
        }
    }
}