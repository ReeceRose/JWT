using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Services
{
    public class UserService : IUser
    {
        private readonly IdentityDbContext _context;

        public UserService(IdentityDbContext context)
        {
            _context = context;
        }

        public IEnumerable<IdentityUser> GetAllUsers()
        {
            return _context.Users;
        }

        public async Task<IdentityUser> GetUserByUsernameAsync(string userName)
        {
            return await GetAllUsers().ToAsyncEnumerable().FirstOrDefault(u => u.UserName == userName);
        }
    }
}
