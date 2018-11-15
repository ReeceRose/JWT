using Core.Models.Transfer;
using MediatR;

namespace Core.Queries.Users
{
    public class GetUserByUsernameQuery : IRequest<ApplicationUser>
    {
        public string Username { get; }
        public GetUserByUsernameQuery(string username)
        {
            Username = username;
        }
    }
}
