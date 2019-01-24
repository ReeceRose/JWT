using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JWT.Application.User.Model;
using JWT.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JWT.Application.User.Query.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<ApplicationUserDto>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<ApplicationUserDto>> Handle(GetAllUsersQuery request,
            CancellationToken cancellationToken)
        {
            if (request.PaginationModel == null) return _mapper.Map<List<ApplicationUserDto>>(await _dbContext.Users.ToListAsync(cancellationToken));
            var users = _dbContext.Users.OrderBy(u => u.Email);
            request.PaginationModel.Count = users.Count();
            var result = await users.
                Skip((request.PaginationModel.CurrentPage - 1) * request.PaginationModel.Count)
                .Take(request.PaginationModel.PageSize)
                .ToListAsync(cancellationToken);
            return _mapper.Map<List<ApplicationUserDto>>(result);
        }
    }
}
