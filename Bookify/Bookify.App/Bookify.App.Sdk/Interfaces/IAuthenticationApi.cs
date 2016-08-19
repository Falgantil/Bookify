using System.Threading.Tasks;

using Bookify.Common.Commands.Auth;
using Bookify.Common.Models;

namespace Bookify.App.Sdk.Interfaces
{
    public interface IAuthenticationApi
    {
        Task<AuthTokenDto> Authenticate(AuthenticateCommand command);
    }
}