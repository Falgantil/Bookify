using System.Threading.Tasks;

using Bookify.Common.Commands.Auth;
using Bookify.Common.Models;
using Bookify.DataAccess.Models;

namespace Bookify.DataAccess.Interfaces.Repositories
{
    public interface IAuthenticationRepository
    {
        Task<string> Login(AuthenticateCommand command);

        Task<PersonDto> Register(CreateAccountCommand command);
    }
}
