using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using JWT.Application.User.Query.GenerateLoginToken;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace JWT.Tests.Core.Application.User.Query.GenerateLoginToken
{
    public class GenerateLoginTokenTest
    {
        public Mock<IConfiguration> Configuration;

        private List<Claim> Claims { get; }
        public GenerateLoginTokenQueryHandler Handler { get; }

        public GenerateLoginTokenTest()
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
            Handler = new GenerateLoginTokenQueryHandler(Configuration.Object);
        }
        [Fact]
        public void GenerateTokenQuery_ShouldReturnToken()
        {
            // Act
            var token = Handler.Handle(new GenerateLoginTokenQuery(Claims), CancellationToken.None).Result;
            // Assert
            Assert.NotNull(token);
            Assert.NotEmpty(token);
        }

        [Fact]
        public void GenerateTokenQuery_IssuerIsValid()
        {
            // Act
            var token = Handler.Handle(new GenerateLoginTokenQuery(Claims), CancellationToken.None).Result;
            var test = new JwtSecurityToken(token);
            // Assert
            Assert.Equal(Configuration.Object["JWT:Issuer"], new JwtSecurityToken(token).Issuer);
        }

        [Fact]
        public void GenerateTokenQuery_AudienceIsValid()
        {
            // Act
            var token = Handler.Handle(new GenerateLoginTokenQuery(Claims), CancellationToken.None).Result;
            // Assert
            Assert.Equal(Configuration.Object["JWT:Audience"], new JwtSecurityToken(token).Audiences.First());
        }

        [Fact]
        public void GenerateTokenQuery_HasPassedClaims()
        {
            // Act
            var token = Handler.Handle(new GenerateLoginTokenQuery(Claims), CancellationToken.None).Result;
            // Assert
            Assert.Contains(Claims.First().Value, new JwtSecurityToken(token).Claims.First(claim => claim.Value == Claims.First().Value).Value);
        }
    }
}