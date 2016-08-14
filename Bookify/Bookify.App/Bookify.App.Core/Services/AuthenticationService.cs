using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Bookify.App.Core.Helpers;
using Bookify.App.Core.Interfaces.Services;
using Bookify.App.Core.Models;

using ModernHttpClient;

namespace Bookify.App.Core.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public AccountModel LoggedOnAccount { get; private set; }

        public async Task<AccountModel> Authenticate(string username, string password)
        {
            await Task.Delay(1500);
            using (var httpClient = new HttpClient(new NativeMessageHandler()))
            {
                var response = await httpClient.GetAsync("https://www.google.dk/");
                await response.ThrowIfUnsuccessful();
            }

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