using JWT.Domain.Entities;
using MediatR;

namespace JWT.Application.User.Command.UpdateUser
{
    public class UpdateUserCommand : IRequest<bool>
    {
        public UpdateUserCommand(ApplicationUser user) => User = user;

        public ApplicationUser User { get; }
    }
}
