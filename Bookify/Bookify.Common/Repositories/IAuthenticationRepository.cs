using System.Threading.Tasks;
using Bookify.Common.Commands.Auth;
using Bookify.Common.Models;

namespace Bookify.Common.Repositories
{
    public interface IAuthenticationRepository
    {
        Task<string> Login(AuthenticateCommand command);

        Task<PersonDto> Register(CreateAccountCommand command);
    }
}
