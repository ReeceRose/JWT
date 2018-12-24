using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace JWT.Application.User.Command.AddUserClaim
{
    public class AddUserClaimCommandHandler : IRequestHandler<AddUserClaimCommand, bool>
    {
        public Task<bool> Handle(AddUserClaimCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
