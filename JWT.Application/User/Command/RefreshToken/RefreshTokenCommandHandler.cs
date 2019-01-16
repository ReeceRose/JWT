using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace JWT.Application.User.Command.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, string>
    {
        public Task<string> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
