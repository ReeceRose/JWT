using System;
using JWT.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace JWT.Tests.Helpers
{
    public class MockUserManager : UserManager<ApplicationUser>
    {
        public MockUserManager()
            : base(new Mock<IUserStore<ApplicationUser>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<ApplicationUser>>().Object,
                new IUserValidator<ApplicationUser>[0],
                new IPasswordValidator<ApplicationUser>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<ApplicationUser>>>().Object)
        { }

        //public override Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
        //{
        //    return Task.FromResult(IdentityResult.Success);
        //}

        //public override Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string role)
        //{
        //    return Task.FromResult(IdentityResult.Success);
        //}

        //public override Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user)
        //{
        //    return Task.FromResult(Guid.NewGuid().ToString());
        //}

    }
}
