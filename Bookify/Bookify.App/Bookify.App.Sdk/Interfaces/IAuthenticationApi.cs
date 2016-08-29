using System.Threading.Tasks;

using Bookify.Common.Commands.Auth;
using Bookify.Common.Models;

namespace Bookify.App.Sdk.Interfaces
{
    public interface IAuthenticationApi
    {
        /// <summary>
        /// Authenticates using the specified <see cref="command"/>.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        Task<AuthTokenDto> Authenticate(AuthenticateCommand command);

        /// <summary>
        /// Deauthenticates this instance, disposing of the Auth token.
        /// </summary>
        /// <returns></returns>
        Task Deauthenticate();

        /// <summary>
        /// Authenticates the using the specified <see cref="authToken"/>.
        /// </summary>
        /// <param name="authToken">The authentication token.</param>
        /// <returns></returns>
        Task Authenticate(AuthTokenDto authToken);

        /// <summary>
        /// Registers on the server using data in the specified <see cref="command"/>.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        Task<AuthTokenDto> Register(CreateAccountCommand command);
    }
}