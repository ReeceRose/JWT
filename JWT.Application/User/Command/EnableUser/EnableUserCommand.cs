using MediatR;

namespace JWT.Application.User.Command.EnableUser
{
    public class EnableUserCommand : IRequest<bool>
    {
        public EnableUserCommand(string userId) => UserId = userId;

        public string UserId { get; }
    }
}
