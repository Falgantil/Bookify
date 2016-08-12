using System.Threading.Tasks;

using Bookify.App.Core.Models;

namespace Bookify.App.Core.Interfaces.Services
{
    public interface IAuthenticationService
    {
        AccountModel LoggedOnAccount { get; }

        Task<AccountModel> Authenticate(string username, string password);
    }
}