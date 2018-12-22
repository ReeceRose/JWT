using MediatR;

namespace JWT.Application.User.Query.LoginUser.External.Facebook
{
    public class LoginUserExternalFacebookQuery : IRequest<string>
    {
        public LoginUserExternalFacebookQuery(string accessToken) => AccessToken = accessToken;

        public string AccessToken { get; set; }
    }
}
