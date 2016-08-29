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
        /// <summary>
        /// The authorization header
        /// </summary>
        private const string HeaderAuthorization = "Authorization";

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationApi"/> class.
        /// </summary>
        public AuthenticationApi()
            : base(ApiConfig.AuthRoot)
        {
        }

        /// <summary>
        /// Authenticates using the specified <see cref="command" />.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        public async Task<AuthTokenDto> Authenticate(AuthenticateCommand command)
        {
            var request = new RequestBuilder()
                .BaseUri(this.CombineUrl("login"))
                .Method(HttpMethod.Post)
                .JsonContent(command);
            var response = await this.ExecuteRequest(request);
            var json = await response.Content.ReadAsStringAsync();
            var authToken = JsonConvert.DeserializeObject<AuthTokenDto>(json);

            DefaultHeaders[HeaderAuthorization] = $"JWT {authToken.Token}";

            return authToken;
        }

        /// <summary>
        /// Deauthenticates this instance, disposing of the Auth token.
        /// </summary>
        /// <returns></returns>
        public async Task Deauthenticate()
        {
            if (DefaultHeaders.ContainsKey(HeaderAuthorization))
            {
                DefaultHeaders.Remove(HeaderAuthorization);
            }
        }

        /// <summary>
        /// Authenticates the using the specified <see cref="authToken" />.
        /// </summary>
        /// <param name="authToken">The authentication token.</param>
        /// <returns></returns>
        public async Task Authenticate(string authToken)
        {
            if (!DefaultHeaders.ContainsKey(HeaderAuthorization))
            {
                DefaultHeaders.Add(HeaderAuthorization, $"JWT {authToken}");
            }
        }

        /// <summary>
        /// Registers on the server using data in the specified <see cref="command" />.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        public async Task<AuthTokenDto> Register(CreateAccountCommand command)
        {
            var request = new RequestBuilder()
                .BaseUri(this.CombineUrl("register"))
                .Method(HttpMethod.Post)
                .JsonContent(command);
            var response = await this.ExecuteRequest(request);
            var json = await response.Content.ReadAsStringAsync();
            var authToken = JsonConvert.DeserializeObject<AuthTokenDto>(json);

            DefaultHeaders[HeaderAuthorization] = $"JWT {authToken.Token}";

            return authToken;
        }
    }
}