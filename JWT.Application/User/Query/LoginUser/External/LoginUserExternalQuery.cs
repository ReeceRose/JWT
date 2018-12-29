using MediatR;

namespace JWT.Application.User.Query.LoginUser.External
{
    public class LoginUserExternalQuery : IRequest<string>
    {
        public LoginUserExternalQuery(string email, string accessToken)
        {
            Email = email;
            AccessToken = accessToken;
        }
        public string Email { get; set; }
        public string AccessToken { get; }
    }
}
