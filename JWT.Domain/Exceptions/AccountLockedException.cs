using System;

namespace JWT.Domain.Exceptions
{
    public class AccountLockedException : Exception
    {
        public AccountLockedException() : base("Your account is locked")
        {
            
        }
        
        public AccountLockedException(string message) : base(message)
        {
            
        }
    }
}