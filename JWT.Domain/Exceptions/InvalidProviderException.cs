using System;

namespace JWT.Domain.Exceptions
{
    public class InvalidProviderException : Exception
    {
        public InvalidProviderException() : base("Invalid provider")
        {
            
        }

        public InvalidProviderException(string message) : base(message)
        {
            
        }
    }
}
