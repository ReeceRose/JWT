using MediatR;
using Microsoft.AspNetCore.Identity;

namespace JWT.Application.User.Command.CreateUser
{
    public class CreateUserCommand : IRequest<IdentityResult>
    {
        public CreateUserCommand(IdentityUser user, string password)
        {
            User = user;
            Password = password;
        }

        public IdentityUser User { get; }
        public string Password { get; set; }
    }
}
