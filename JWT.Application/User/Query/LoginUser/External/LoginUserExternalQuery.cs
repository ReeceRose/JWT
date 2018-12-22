using MediatR;

namespace JWT.Application.User.Query.LoginUser.External
{
    public class LoginUserExternalQuery : IRequest<string>
    {
        public LoginUserExternalQuery(string accessToken, string provider)
        {
            AccessToken = accessToken;
            Provider = provider;
        }

        public string AccessToken { get; }
        public string Provider { get; }
    }
}
