using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JWT.Application.User.Model;
using JWT.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JWT.Application.User.Query.GetPaginatedUsers
{
    public class GetPaginatedUsersQueryHandler : IRequestHandler<GetPaginatedUsersQuery, GetPaginatedUsersDto>
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public GetPaginatedUsersQueryHandler(IMapper mapper, ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<GetPaginatedUsersDto> Handle(GetPaginatedUsersQuery request, CancellationToken cancellationToken)
        {
            var users = _context.Users.OrderBy(u => u.Email);
            request.PaginationModel.Count = users.Count();
            var result = await users.
                Skip((request.PaginationModel.CurrentPage - 1) * request.PaginationModel.PageSize)
                .Take(request.PaginationModel.PageSize)
                .ToListAsync(cancellationToken);
            return new GetPaginatedUsersDto()
            {
                PaginationModel = request.PaginationModel,
                Users = _mapper.Map<List<ApplicationUserDto>>(result)
            };
        }
    }
}
