using System.Threading;
using System.Threading.Tasks;
using JWT.Application.User.Command.CreateUser;
using JWT.Application.User.Query.GenerateLoginToken;
using JWT.Application.User.Query.GetUserByEmail;
using JWT.Application.User.Query.GetUserClaim;
using JWT.Application.Utilities;
using JWT.Domain.Entities;
using JWT.Domain.Exceptions;
using MediatR;

namespace JWT.Application.User.Query.LoginUser.External
{
    public class LoginUserExternalQueryHandler : IRequestHandler<LoginUserExternalQuery, string>
    {
        private readonly IMediator _mediator;

        public LoginUserExternalQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<string> Handle(LoginUserExternalQuery request, CancellationToken cancellationToken)
        {
            var user = _mediator.Send(new GetUserByEmailQuery(request.Email), cancellationToken).Result;

            if (user == null)
            {
                var newUser = new ApplicationUser()
                {
                    Email = request.Email,
                    UserName = request.Email,
                    EmailConfirmed = true
                };
                
                var result = await _mediator.Send(new CreateUserCommand(newUser, GenerateRandomPassword.SecurePassword()), cancellationToken);

                if (!result.Succeeded)
                {
                    throw new InvalidRegisterException("Failed to register account with Facebook");
                }
            }

            var claims = _mediator.Send(new GetUserClaimQuery(user), cancellationToken).Result;

            return _mediator.Send(new GenerateLoginTokenQuery(claims), cancellationToken).Result; ;
        }
    }
}
