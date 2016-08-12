using System.Threading.Tasks;

using Bookify.App.Core.Interfaces.Services;
using Bookify.App.Core.Models;

namespace Bookify.App.Core.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public AccountModel LoggedOnAccount { get; private set; }

        public async Task<AccountModel> Authenticate(string username, string password)
        {
            await Task.Delay(1500);
            var accountModel = new AccountModel
            {
                FirstName = "Bjarke",
                IsSubscribed = true,
                LastName = "Søgaard"
            };
            this.LoggedOnAccount = accountModel;
            return accountModel;
        }
    }
}