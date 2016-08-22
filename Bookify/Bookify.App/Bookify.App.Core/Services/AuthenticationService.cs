using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using Bookify.App.Core.Interfaces.Services;
using Bookify.App.Core.Models;
using Bookify.App.Sdk.Implementations;
using Bookify.App.Sdk.Interfaces;
using Bookify.Common.Commands.Auth;
using Bookify.Common.Models;

namespace Bookify.App.Core.Services
{
    public class AuthenticationService : BaseModel, IAuthenticationService
    {
        private readonly IAuthenticationApi authApi;

        private readonly IPersonApi personApi;

        public AuthenticationService(IAuthenticationApi authApi, IPersonApi personApi)
        {
            this.authApi = authApi;
            this.personApi = personApi;
        }

        /// <summary>
        /// Occurs when the authentication state changed (Logged in or out).
        /// </summary>
        public event EventHandler<AccountModel> AuthChanged;

        /// <summary>
        /// Gets the logged on account. Returns null if not currently logged on.
        /// </summary>
        /// <value>
        /// The logged on account.
        /// </value>
        public AccountModel LoggedOnAccount { get; private set; }

        /// <summary>
        /// Called when <see cref="LoggedOnAccount"/> has changed.
        /// </summary>
        private void OnLoggedOnAccountChanged()
        {
            this.AuthChanged?.Invoke(this, this.LoggedOnAccount);
        }

        /// <summary>
        /// Authenticates the user using the provided <see cref="email" /> and <see cref="password" />.
        /// </summary>
        /// <param name="email">The email</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public async Task<PersonDto> Authenticate(string email, string password)
        {
            var token = await this.authApi.Authenticate(new AuthenticateCommand
            {
                Email = email,
                Password = password
            });
            var myself = await this.personApi.GetMyself();

            this.LoggedOnAccount = new AccountModel(token, myself);
            return myself;
        }

        /// <summary>
        /// Deauthenticates the user.
        /// </summary>
        /// <returns></returns>
        public async Task Deauthenticate()
        {
            this.LoggedOnAccount = null;
            await this.authApi.Deauthenticate();
        }

        /// <summary>
        /// Restores from account.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <returns></returns>
        public async Task RestoreFromAccount(AccountModel account)
        {
            this.LoggedOnAccount = account;
            await this.authApi.Authenticate(account.Token);
            var myself = await this.personApi.GetMyself();
            this.LoggedOnAccount.Person = myself;
        }
    }
}