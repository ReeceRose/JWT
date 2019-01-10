using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JWT.Application.User.Command.CreateUser;
using JWT.Application.User.Model;
using JWT.Application.User.Query.GenerateLoginToken;
using JWT.Application.User.Query.GetUserByEmail;
using JWT.Application.User.Query.GetUserClaim;
using JWT.Application.Utilities;
using JWT.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JWT.Application.User.Query.LoginUser.External
{
    public class LoginUserExternalQueryHandler : IRequestHandler<LoginUserExternalQuery, string>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<LoginUserExternalQueryHandler> _logger;

        public LoginUserExternalQueryHandler(IMediator mediator, IMapper mapper, ILogger<LoginUserExternalQueryHandler> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<string> Handle(LoginUserExternalQuery request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<ApplicationUserDto>(_mediator.Send(new GetUserByEmailQuery(request.Email), cancellationToken).Result);

            if (user == null)
            {
                var newUser = new ApplicationUserDto()
                {
                    Email = request.Email,
                    UserName = request.Email,
                    EmailConfirmed = true
                };
                
                var result = await _mediator.Send(new CreateUserCommand(newUser, GenerateRandomPassword.SecurePassword()), cancellationToken);

                if (!result.Succeeded)
                {
                    _logger.LogInformation($"LoginUserExternal: {request.Email}: Failed login: Failed to create a local user");
                    throw new InvalidRegisterException("Failed to register account with third party login provider");
                }
            }

            var claims = _mediator.Send(new GetUserClaimQuery(user), cancellationToken).Result;

            return _mediator.Send(new GenerateLoginTokenQuery(claims), cancellationToken).Result; ;
        }
    }
}
