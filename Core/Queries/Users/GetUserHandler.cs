using AutoMapper;
using Core.Models.Transfer;
using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Queries.Users
{
    public class GetUserHandler : IRequestHandler<GetUserByUsernameQuery, ApplicationUser>
    {
        private readonly IdentityDbContext _context;
        private readonly IMapper _mapper;

        public GetUserHandler(IdentityDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApplicationUser> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<ApplicationUser>(await _context.Users.ToAsyncEnumerable().FirstOrDefault(u => u.UserName == request.Username));
        }
    }
}
