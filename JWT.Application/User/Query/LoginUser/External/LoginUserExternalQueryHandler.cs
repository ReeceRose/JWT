using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using JWT.Application.User.Query.LoginUser.External.Facebook;
using JWT.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace JWT.Application.User.Query.LoginUser.External
{
    public class LoginUserExternalQueryHandler : IRequestHandler<LoginUserExternalQuery, string>
    {
        private readonly IMediator _mediator;
        
        public LoginUserExternalQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<string> Handle(LoginUserExternalQuery request, CancellationToken cancellationToken)
        {
            switch (request.Provider)
            {
                case "Facebook":
                    return _mediator.Send(new LoginUserExternalFacebookQuery(request.AccessToken), cancellationToken);
                default:
                    throw new InvalidProviderException();
            }
        }
    }
}
