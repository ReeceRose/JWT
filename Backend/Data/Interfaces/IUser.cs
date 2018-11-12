using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Data.Interfaces
{
    public interface IUser
    {
        IEnumerable<IdentityUser> GetAllUsers();
        Task<IdentityUser> GetUserByUsernameAsync(string userName);
    }
}
