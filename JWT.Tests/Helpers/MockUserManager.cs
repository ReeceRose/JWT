using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace JWT.Tests.Helpers
{
    public class MockUserManager : UserManager<IdentityUser>
    {
        public MockUserManager()
            : base(new Mock<IUserStore<IdentityUser>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<IdentityUser>>().Object,
                new IUserValidator<IdentityUser>[0],
                new IPasswordValidator<IdentityUser>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<IdentityUser>>>().Object)
        { }

        //public override Task<IdentityResult> CreateAsync(IdentityUser user, string password)
        //{
        //    return Task.FromResult(IdentityResult.Success);
        //}

        //public override Task<IdentityResult> AddToRoleAsync(IdentityUser user, string role)
        //{
        //    return Task.FromResult(IdentityResult.Success);
        //}

        //public override Task<string> GenerateEmailConfirmationTokenAsync(IdentityUser user)
        //{
        //    return Task.FromResult(Guid.NewGuid().ToString());
        //}

    }
}
