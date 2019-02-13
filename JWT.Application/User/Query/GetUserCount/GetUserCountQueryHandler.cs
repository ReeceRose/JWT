using System.Threading;
using System.Threading.Tasks;
using JWT.Application.User.Query.GetAllUsers;
using MediatR;

namespace JWT.Application.User.Query.GetUserCount
{
    public class GetUserCountQueryHandler : IRequestHandler<GetUserCountQuery, int>
    {
        private readonly IMediator _mediator;

        public GetUserCountQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<int> Handle(GetUserCountQuery request, CancellationToken cancellationToken)
        {
            var users = await _mediator.Send(new GetAllUsersQuery(), cancellationToken);
            return users?.Count ?? 0;
        }
    }
}
