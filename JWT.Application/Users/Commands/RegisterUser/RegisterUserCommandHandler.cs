using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JWT.Application.Token.Query.GetToken;
using JWT.Application.Users.Queries.GetUserByEmail;
using JWT.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace JWT.Application.Users.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;

        public RegisterUserCommandHandler(IMediator mediator, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            _mediator = mediator;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<bool> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mediator.Send(new GetUserByEmailQuery(request.Email), cancellationToken).Result;

            if (user != null)
            {
                throw new AccountAlreadyExistsException(request.Email);
            }

            // TODO: Refactor out IdentityUser to ApplicationUser
            user = _mapper.Map<IdentityUser>(request);

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                throw new InvalidRegisterException();
            }
            // NOTE: DO NOT DO THIS!!
            if (request.IsAdmin)
            {
                await _userManager.AddClaimAsync(user, new Claim("Administrator", ""));
            }
            // Return below if you want to sign in user right away
            //return await _mediator.Send(new GetTokenQuery(_userManager.GetClaimsAsync(user).Result), cancellationToken: cancellationToken);
            return await Task.FromResult(result.Succeeded);
        }
    }
}