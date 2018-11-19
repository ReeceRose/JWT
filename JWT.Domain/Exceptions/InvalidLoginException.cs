using System;

namespace JWT.Domain.Exceptions
{
    public class InvalidLoginException : Exception
    {
        public InvalidLoginException() : base("Invalid login attempt. Please check your email and password")
        {

        }
    }
}