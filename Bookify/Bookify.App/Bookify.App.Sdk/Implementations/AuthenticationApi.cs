using System.Net.Http;
using System.Threading.Tasks;

using Bookify.App.Sdk.Interfaces;
using Bookify.Common.Commands.Auth;
using Bookify.Common.Models;

using Newtonsoft.Json;

namespace Bookify.App.Sdk.Implementations
{
    public class AuthenticationApi : BaseApi, IAuthenticationApi
    {
        private const string AuthenticationHeader = "Authentication";

        public AuthenticationApi()
            : base(ApiConfig.AuthRoot)
        {
        }

        public async Task<AuthTokenDto> Authenticate(AuthenticateCommand command)
        {
            var request = new RequestBuilder()
                .BaseUri(this.CombineUrl("login"))
                .Method(HttpMethod.Post)
                .JsonContent(command);
            var response = await this.ExecuteRequest(request);
            var json = await response.Content.ReadAsStringAsync();
            var authToken = JsonConvert.DeserializeObject<AuthTokenDto>(json);

            DefaultHeaders[AuthenticationHeader] = $"JWT {authToken.Token}";

            return authToken;
        }

        public async Task Deauthenticate()
        {
            if (DefaultHeaders.ContainsKey(AuthenticationHeader))
            {
                DefaultHeaders.Remove(AuthenticationHeader);
            }
        }

        public async Task Authenticate(AuthTokenDto authToken)
        {
            if (!DefaultHeaders.ContainsKey(AuthenticationHeader))
            {
                DefaultHeaders.Add(AuthenticationHeader, $"JWT {authToken.Token}");
            }
        }
    }
}