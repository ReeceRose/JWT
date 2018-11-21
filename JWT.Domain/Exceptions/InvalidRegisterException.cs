using System;

namespace JWT.Domain.Exceptions
{
    public class InvalidRegisterException : Exception
    {
        public InvalidRegisterException() : base("Failed to create account. Please try again")
        {
            
        }
        
        public InvalidRegisterException(string message) : base(message)
        {
            
        }
    }
}
