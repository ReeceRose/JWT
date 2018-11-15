using MediatR;

namespace Core.Requests.Authentication.Register
{
    public class UserRegisterRequest : IRequest<bool>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}
