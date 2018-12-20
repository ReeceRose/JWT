using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JWT.Application.Interfaces;
using JWT.Application.User.Query.GenerateEmailConfirmation.Email;
using JWT.Application.User.Query.GetUserByEmail;
using JWT.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace JWT.Application.User.Command.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly INotificationService _notificationService;
        private readonly IConfiguration _configuration;

        public RegisterUserCommandHandler(IMediator mediator, IMapper mapper, UserManager<IdentityUser> userManager, INotificationService notificationService, IConfiguration configuration)
        {
            _mediator = mediator;
            _mapper = mapper;
            _userManager = userManager;
            _notificationService = notificationService;
            _configuration = configuration;
        }

        public async Task<bool> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var email = request.Email;
            var user = _mediator.Send(new GetUserByEmailQuery(email), cancellationToken).Result;

            if (user != null)
            {
                throw new AccountAlreadyExistsException(email);
            }

            // TODO: Refactor out IdentityUser to ApplicationUser
            user = _mapper.Map<IdentityUser>(request);

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                throw new InvalidRegisterException();
            }

            await _mediator.Send(new GenerateEmailConfirmationEmailQuery(email), cancellationToken);

            // NOTE: DO NOT DO THIS!!
            if (request.IsAdmin)
            {
                await _userManager.AddClaimAsync(user, new Claim("Administrator", ""));
            }

            return await Task.FromResult(result.Succeeded);
        }
    }
}