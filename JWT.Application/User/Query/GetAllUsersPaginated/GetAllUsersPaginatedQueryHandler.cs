using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JWT.Application.User.Model;
using JWT.Application.User.Query.GetAllUsers;
using JWT.Application.User.Query.GetPaginatedResults;
using MediatR;

namespace JWT.Application.User.Query.GetAllUsersPaginated
{
    public class GetAllUsersPaginatedQueryHandler : IRequestHandler<GetAllUsersPaginatedQuery, PaginatedUsersDto>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public GetAllUsersPaginatedQueryHandler(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<PaginatedUsersDto> Handle(GetAllUsersPaginatedQuery request, CancellationToken cancellationToken)
        {
            var allUsers = await _mediator.Send(new GetAllUsersQuery(), cancellationToken);
            var orderedUsers = allUsers.OrderBy(u => u.Email);
            request.PaginationModel.Count = orderedUsers.Count();
            var paginatedUsers = orderedUsers.Skip((request.PaginationModel.CurrentPage - 1) * request.PaginationModel.PageSize)
                .Take(request.PaginationModel.PageSize).ToList();
            var result = _mapper.Map<List<ApplicationUserDto>>(paginatedUsers.ToList());

            return await _mediator.Send(new GetPaginatedResultsQuery(result, request.PaginationModel), cancellationToken);
        }
    }
}
