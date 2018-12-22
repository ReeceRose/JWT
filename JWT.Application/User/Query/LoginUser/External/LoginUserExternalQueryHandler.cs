using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace JWT.Application.User.Query.LoginUser.External
{
    public class LoginUserExternalQueryHandler : IRequestHandler<LoginUserExternalQuery, string>
    {
        private readonly IConfiguration _configuration;
        private static readonly HttpClient Client = new HttpClient();
        
        public LoginUserExternalQueryHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<string> Handle(LoginUserExternalQuery request, CancellationToken cancellationToken)
        {
            //var appAccessTokenResponse = await  Client.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id={_configuration['Facebook:AppId']}")
            throw new System.NotImplementedException();
        }
    }
}
