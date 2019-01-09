using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JWT.Application.User.Query.GetUserByEmail;
using JWT.Domain.Entities;
using JWT.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace JWT.Application.User.Command.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;

        public ResetPasswordCommandHandler(IMediator mediator, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        public async Task<bool> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = _mediator.Send(new GetUserByEmailQuery(request.Email), cancellationToken).Result;

            if (user == null)
            {
                throw new InvalidUserException();
            }

            var result = await _userManager.ResetPasswordAsync(user, Base64UrlEncoder.Decode(request.Token), request.Password);
            if (!result.Succeeded)
            {
                throw new FailedToResetPassword();
            }
            return await Task.FromResult(true);
        }
    }
}