using JWT.Application.User.Model;
using JWT.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace JWT.Application.User.Command.CreateUser
{
    public class CreateUserCommand : IRequest<IdentityResult>
    {
        public CreateUserCommand(ApplicationUserDto user, string password)
        {
            User = user;
            Password = password;
        }

        public ApplicationUserDto User { get; }
        public string Password { get; set; }
    }
}
