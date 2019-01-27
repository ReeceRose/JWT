using JWT.Application.User.Model;
using MediatR;

namespace JWT.Application.User.Query.SearchUsersByEmail
{
    public class SearchUsersByEmailQuery : IRequest<PaginatedUsersDto>
    {
        public SearchUsersByEmailQuery(string email) => Email = email;

        public string Email { get; set; }
    }
}
