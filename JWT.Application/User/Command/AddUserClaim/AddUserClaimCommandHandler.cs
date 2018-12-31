using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JWT.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace JWT.Application.User.Command.AddUserClaim
{
    public class AddUserClaimCommandHandler : IRequestHandler<AddUserClaimCommand, bool>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public AddUserClaimCommandHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<bool> Handle(AddUserClaimCommand request, CancellationToken cancellationToken)
        {
            var result = await _userManager.AddClaimAsync(_mapper.Map<ApplicationUser>(request.User), new Claim(request.Key, request.Value));
            return result.Succeeded;
        }
    }
}
