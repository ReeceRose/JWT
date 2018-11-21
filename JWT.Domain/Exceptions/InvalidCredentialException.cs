using System;

namespace JWT.Domain.Exceptions
{
    public class InvalidCredentialException : Exception
    {
        public InvalidCredentialException() : base("Invalid username or password. Please try again")
        {
            
        }
        public InvalidCredentialException(string message) : base(message)
        {
            
        }
    }
}