using JWT.Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace JWT.Tests.Helpers
{
    public class MockSignInManager : SignInManager<ApplicationUser>
    {
        public MockSignInManager()
            : base(new Mock<MockUserManager>().Object,
                new Mock<IHttpContextAccessor>().Object,
                new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<ILogger<SignInManager<ApplicationUser>>>().Object,
                new Mock<IAuthenticationSchemeProvider>().Object)
        { }
    }
}