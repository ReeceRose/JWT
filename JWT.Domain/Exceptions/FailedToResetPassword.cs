using System;

namespace JWT.Domain.Exceptions
{
    public class FailedToResetPassword : Exception
    {
        public FailedToResetPassword() : base("Failed to reset password. Try again")
        {

        }

        public FailedToResetPassword(string message) : base(message)
        {

        }
    }
}
