using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JWT.Tests.Context
{
    public class ContextFactory
    {
        public static IdentityDbContext Create()
        {
            var options = new DbContextOptionsBuilder<IdentityDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new IdentityDbContext(options);
            context.Database.EnsureCreated();
            context.Users.Add(new IdentityUser() { Email = "test@test.ca", Id = "123", UserName = "test-user" });
            context.SaveChanges();
            return context;
        }

        public static void Destroy(IdentityDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}