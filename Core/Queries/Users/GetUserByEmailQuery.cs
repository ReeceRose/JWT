using Core.Models.Transfer;
using MediatR;

namespace Core.Queries.Users
{
    public class GetUserByEmailQuery : IRequest<ApplicationUser>
    {
        public string Email { get; }
        public GetUserByEmailQuery(string email) => Email = email;
    }
}
