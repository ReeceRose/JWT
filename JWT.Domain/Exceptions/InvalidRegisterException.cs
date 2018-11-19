using System;

namespace JWT.Domain.Exceptions
{
    public class InvalidRegisterException : Exception
    {
        public InvalidRegisterException() : base("Failed to create user. Please try again")
        {
            
        }
    }
}
