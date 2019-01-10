using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JWT.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace JWT.Application.User.Command.AddUserClaim
{
    public class AddUserClaimCommandHandler : IRequestHandler<AddUserClaimCommand, bool>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger<AddUserClaimCommandHandler> _logger;

        public AddUserClaimCommandHandler(UserManager<ApplicationUser> userManager, IMapper mapper, ILogger<AddUserClaimCommandHandler> logger)
        {
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> Handle(AddUserClaimCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Add Claim: {request.User.Email}: Adding claim ({request.Key}, {request.Value})");
            var result = await _userManager.AddClaimAsync(_mapper.Map<ApplicationUser>(request.User), new Claim(request.Key, request.Value));
            return result.Succeeded;
        }
    }
}
