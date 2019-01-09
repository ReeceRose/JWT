using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using JWT.Application.User.Model;
using JWT.Application.User.Query.GetUserById;
using JWT.Domain.Entities;
using JWT.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace JWT.Application.User.Command.ConfirmUserEmail
{
    public class ConfirmUserEmailCommandHandler : IRequestHandler<ConfirmUserEmailCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public ConfirmUserEmailCommandHandler(IMediator mediator, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _mediator = mediator;
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<bool> Handle(ConfirmUserEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _mediator.Send(new GetUserByIdQuery(Base64UrlEncoder.Decode(request.UserId)), cancellationToken);

            if (user == null)
            {
                throw new InvalidUserException();
            }
            
            var result = await _userManager.ConfirmEmailAsync(user,
                Base64UrlEncoder.Decode(request.Token));

            if (!result.Succeeded)
            {
                throw new Exception("Failed to confirm email");
            }

            return await Task.FromResult(result.Succeeded);
        }
    }
}
