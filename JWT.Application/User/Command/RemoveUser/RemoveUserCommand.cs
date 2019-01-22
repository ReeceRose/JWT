using MediatR;

namespace JWT.Application.User.Command.RemoveUser
{
    public class RemoveUserCommand : IRequest<bool>
    {
        public RemoveUserCommand(string userId) => UserId = userId;

        public string UserId { get; }
    }
}
