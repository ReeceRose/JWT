using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using JWT.Application.User.Model.LoginUser.External.Facebook;
using JWT.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace JWT.Application.User.Query.LoginUser.External.Facebook
{
    public class LoginUserExternalFacebookQueryHandler : IRequestHandler<LoginUserExternalFacebookQuery, string>
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        private static readonly HttpClient Client = new HttpClient();

        public LoginUserExternalFacebookQueryHandler(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator;
            _configuration = configuration;
        }

        public async Task<string> Handle(LoginUserExternalFacebookQuery request, CancellationToken cancellationToken)
        {
            var appAccessTokenResponse = await Client.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id={_configuration["Facebook:AppId"]}&client_secret={_configuration["Facebook:AppSecret"]}&grant_type=client_credentials");
            var appAccessToken = JsonConvert.DeserializeObject<FacebookAccessToken>(appAccessTokenResponse);
            var userAccessTokenValidationResponse = await Client.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={request.AccessToken}&access_token={appAccessToken.AccessToken}");
            var userAccessTokenValidation = JsonConvert.DeserializeObject<FacebookAccessTokenValidation>(userAccessTokenValidationResponse);

            if (!userAccessTokenValidation.Data.IsValid)
            {
                throw new InvalidExternalProviderToken();
            }
            throw new System.NotImplementedException();
        }
    }
}
