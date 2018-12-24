using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using JWT.Application.User.Command.CreateUser;
using JWT.Application.User.Model.LoginUser.External.Facebook;
using JWT.Application.User.Query.GenerateLoginToken;
using JWT.Application.User.Query.GetUserByEmail;
using JWT.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
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

            var userInfoResponse = await Client.GetStringAsync($"https://graph.facebook.com/v2.8/me?fields=id,email,name&access_token={request.AccessToken}");
            var userInfo = JsonConvert.DeserializeObject<FacebookAccessUserData>(userInfoResponse);

            var user = _mediator.Send(new GetUserByEmailQuery(userInfo.Email), cancellationToken);

            if (user == null)
            {
                var newUser = new IdentityUser()
                {
                    Email = userInfo.Email,
                    UserName = userInfo.Email
                };

                var result = await _mediator.Send(new CreateUserCommand(newUser, Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8)), cancellationToken);

                if (!result.Succeeded)
                {
                    throw new InvalidRegisterException("Failed to register account with Facebook");
                }
            }

            var localUser = _mediator.Send(new GetUserByEmailQuery(userInfo.Email), cancellationToken).Result;

            if (localUser == null)
            {
                throw new InvalidUserException("Failed to load Facebook user");
            }

            return _mediator.Send(new GenerateLoginTokenQuery(null), cancellationToken).Result;;
        }
    }
}
