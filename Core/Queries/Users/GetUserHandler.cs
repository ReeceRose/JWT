using AutoMapper;
using Core.Models.Transfer;
using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Core.Queries.Users
{
    public class GetUserHandler : IRequestHandler<GetUserByEmailQuery, ApplicationUser>
    {
        private readonly IdentityDbContext _context;
        private readonly IMapper _mapper;

        public GetUserHandler(IdentityDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApplicationUser> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<ApplicationUser>(await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken));
        }
    }
}
