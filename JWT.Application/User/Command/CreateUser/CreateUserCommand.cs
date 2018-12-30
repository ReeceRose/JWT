using JWT.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace JWT.Application.User.Command.CreateUser
{
    public class CreateUserCommand : IRequest<IdentityResult>
    {
        public CreateUserCommand(ApplicationUser user, string password)
        {
            User = user;
            Password = password;
        }

        public ApplicationUser User { get; }
        public string Password { get; set; }
    }
}
