﻿namespace JWT.Application.Requests.Authentication.Login
{
    public class UserLoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}