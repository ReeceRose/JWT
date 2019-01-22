using System;

namespace JWT.Application.User.Model
{
    public class ApplicationUserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool AccountEnabled { get; set; }
        public DateTime DateJoined { get; set; }
    }
}
