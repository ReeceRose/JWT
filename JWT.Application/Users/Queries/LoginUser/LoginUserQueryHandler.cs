using System.Threading;
using System.Threading.Tasks;
using JWT.Application.Token.Commands.GenerateToken;
using JWT.Application.Users.Queries.GetUserByEmail;
using JWT.Domain.Exceptions;
using MediatR;

namespace JWT.Application.Users.Queries.LoginUser
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, string>
    {
        private readonly IMediator _mediator;

        public LoginUserQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public Task<string> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var user = _mediator.Send(new GetUserByEmailQuery(request.Email), cancellationToken).Result;
            
            if (user == null)
            {
                throw new InvalidLoginException();
            }
            // TODO: Add more logic
            return _mediator.Send(new GenerateTokenCommand(), cancellationToken: cancellationToken);
        }
    }
}
