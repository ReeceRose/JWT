using System;
using Microsoft.AspNetCore.Identity;

namespace JWT.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        // Default ApplicationUser Properties
        /*
            Access Failed Count
            Concurrency Stamp
            Email
            Email Confirmed
            Id
            Lockout Enabled
            Lockout End
            Normalized Email
            Normalized User Name
            Password Hash
            Phone Number
            Phone Number Confirmed
            Security Stamp
            Two Factor Enabled
            Username
         */
        public DateTime DateJoined { get; set; }
    }
}
