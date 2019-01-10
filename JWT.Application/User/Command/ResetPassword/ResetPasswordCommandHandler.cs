using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JWT.Application.User.Query.GetUserByEmail;
using JWT.Domain.Entities;
using JWT.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace JWT.Application.User.Command.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ResetPasswordCommandHandler> _logger;

        public ResetPasswordCommandHandler(IMediator mediator, UserManager<ApplicationUser> userManager, ILogger<ResetPasswordCommandHandler> logger)
        {
            _mediator = mediator;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<bool> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = _mediator.Send(new GetUserByEmailQuery(request.Email), cancellationToken).Result;

            if (user == null)
            {
                _logger.LogInformation($"Reset Password: {request.Email}: Failed reset: User does not exist");
                throw new InvalidUserException();
            }

            var token = Base64UrlEncoder.Decode(request.Token);

            var result = await _userManager.ResetPasswordAsync(user, token, request.Password);
            if (!result.Succeeded)
            {
                _logger.LogInformation($"Reset Password: {request.Email}: Failed reset: {result.Errors}");
                throw new FailedToResetPassword();
            }

            _logger.LogInformation($"Reset Password: {request.Email}: Successful reset");
            return await Task.FromResult(true);
        }
    }
}