using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JWT.Application.User.Model;
using JWT.Application.User.Query.GenerateLoginToken;
using JWT.Application.User.Query.GetUserByEmail;
using JWT.Application.User.Query.GetUserClaim;
using JWT.Domain.Entities;
using JWT.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace JWT.Application.User.Query.LoginUser
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, string>
    {
        private readonly IMediator _mediator;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger<LoginUserQueryHandler> _logger;


        public LoginUserQueryHandler(IMediator mediator, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IMapper mapper, ILogger<LoginUserQueryHandler> logger)
        {
            _mediator = mediator;
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
        }
        
        public async Task<string> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var user = _mediator.Send(new GetUserByEmailQuery(request.Email), cancellationToken).Result;
            
            if (user == null)
            {
                throw new InvalidCredentialException();
            }

            var mappedUser = _mapper.Map<ApplicationUserDto>(user);
            
            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, true);

            if (result.IsLockedOut)
            {
                throw new AccountLockedException();
            }

            await _userManager.AddClaimAsync(user, new Claim("Administrator", ""));

            if (!(result.Succeeded))
            {
                if (!(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    throw new EmailNotConfirmedException();
                }
                throw new InvalidCredentialException();
            }

            var claims = _mediator.Send(new GetUserClaimQuery(mappedUser), cancellationToken).Result;

            _logger.LogInformation($"User: {user.Email} logged in successfully");

            return await _mediator.Send(new GenerateLoginTokenQuery(claims), cancellationToken);
        }
    }
}