using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JWT.Application.Users.Queries.GetUserByEmail
{
    public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, IdentityUser>
    {
        private readonly IdentityDbContext _context;

        public GetUserByEmailQueryHandler(IdentityDbContext context)
        {
            _context = context;
        }

        public async Task<IdentityUser> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            return await _context.Users.FirstOrDefaultAsync(u =>
                string.Equals(u.Email.ToLower(), request.Email.ToLower(), StringComparison.Ordinal), cancellationToken: cancellationToken);
        }
    }
}
