using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JWT.Application.User.Query.GenerateLoginToken;
using JWT.Application.User.Query.GetUserByEmail;
using JWT.Application.User.Query.GetUserClaim;
using JWT.Domain.Entities;
using JWT.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace JWT.Application.User.Query.LoginUser
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, string>
    {
        private readonly IMediator _mediator;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;


        public LoginUserQueryHandler(IMediator mediator, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _mediator = mediator;
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
        }
        
        public async Task<string> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var user = _mediator.Send(new GetUserByEmailQuery(request.Email), cancellationToken).Result;
            
            if (user == null)
            {
                throw new InvalidCredentialException();
            }

            var mappedUser = _mapper.Map<ApplicationUser>(user);
            
            var result = await _signInManager.CheckPasswordSignInAsync(mappedUser, request.Password, true);

            if (result.IsLockedOut)
            {
                throw new AccountLockedException();
            }

            if (!(result.Succeeded))
            {
                if (!(await _userManager.IsEmailConfirmedAsync(mappedUser)))
                {
                    throw new EmailNotConfirmedException();
                }
                throw new InvalidCredentialException();
            }

            var claims = _mediator.Send(new GetUserClaimQuery(user), cancellationToken).Result;

            return await _mediator.Send(new GenerateLoginTokenQuery(claims), cancellationToken);
        }
    }
}