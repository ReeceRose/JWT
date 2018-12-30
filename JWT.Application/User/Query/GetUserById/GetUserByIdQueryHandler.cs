using System.Threading;
using System.Threading.Tasks;
using JWT.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JWT.Application.User.Query.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, IdentityUser>
    {
        private readonly ApplicationDbContext _context;

        public GetUserByIdQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IdentityUser> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken: cancellationToken);
        }
    }
}
