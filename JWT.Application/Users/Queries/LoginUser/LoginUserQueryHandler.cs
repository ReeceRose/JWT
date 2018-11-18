using System.Threading;
using System.Threading.Tasks;
using JWT.Application.Users.Queries.GetUserByEmail;
using MediatR;

namespace JWT.Application.Users.Queries.LoginUser
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, bool>
    {
        private readonly IMediator _mediator;

        public LoginUserQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public Task<bool> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var user = _mediator.Send(new GetUserByEmailQuery(request.Email), cancellationToken).Result;
            
            if (user == null)
            {
                // TODO: Return error
                return Task.FromResult(false);
            }
            // TODO: Add more logic
            return Task.FromResult(true);
        }
    }
}
