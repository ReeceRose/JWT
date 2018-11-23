﻿using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JWT.Application.Token.Query.GetToken;
using JWT.Application.Users.Queries.GetUserByEmail;
using JWT.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace JWT.Application.Users.Queries.LoginUser
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, string>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public LoginUserQueryHandler(IMediator mediator, IMapper mapper, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _mediator = mediator;
            _mapper = mapper;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        
        public async Task<string> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var user = _mediator.Send(new GetUserByEmailQuery(request.Email), cancellationToken).Result;
            
            if (user == null)
            {
                throw new InvalidCredentialException();
            }
            
            // NOTE: Might want to change false to true so it will lock out users
            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, true);
            
            if (!result.Succeeded)
            {
                throw new InvalidCredentialException();
            }
            if (result.IsLockedOut)
            {
                throw new AccountLockedException();
            }
            
            return await _mediator.Send(new GetTokenQuery(_userManager.GetClaimsAsync(user).Result), cancellationToken: cancellationToken);
        }
    }
}