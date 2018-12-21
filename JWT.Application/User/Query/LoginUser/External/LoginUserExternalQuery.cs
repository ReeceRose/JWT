using MediatR;

namespace JWT.Application.User.Query.LoginUser.External
{
    public class LoginUserExternalQuery : IRequest<string>
    {
        public LoginUserExternalQuery(string accessToken) => AccessToken = accessToken;

        public string AccessToken { get; set; }
    }
}
