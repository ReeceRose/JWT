using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Data.Interfaces
{
    public interface IUser
    {
        IEnumerable<IdentityUser> GetAllUsers();
        IdentityUser GetUserByUsername(string userName);
    }
}
