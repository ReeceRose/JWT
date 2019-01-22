using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace JWT.Application.User.Command.RemoveUser
{
    public class RemoveUserCommandHandler : IRequestHandler<RemoveUserCommand, bool>
    {
        public Task<bool> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
