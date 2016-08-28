using System;
using System.Threading.Tasks;

using Bookify.App.Core.Interfaces.Services;
using Bookify.App.Core.Models;
using Bookify.App.Sdk.Interfaces;
using Bookify.Common.Commands.Auth;

namespace Bookify.App.Core.Services
{
    /// <summary>
    /// The Authentication Service implementation.
    /// </summary>
    /// <seealso cref="BaseModel" />
    /// <seealso cref="IAuthenticationService" />
    public class AuthenticationService : BaseModel, IAuthenticationService
    {
        /// <summary>
        /// The authentication API
        /// </summary>
        private readonly IAuthenticationApi authApi;

        /// <summary>
        /// The person service
        /// </summary>
        private readonly IPersonService personService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationService" /> class.
        /// </summary>
        /// <param name="authApi">The authentication API.</param>
        /// <param name="personService">The person service.</param>
        public AuthenticationService(IAuthenticationApi authApi, IPersonService personService)
        {
            this.authApi = authApi;
            this.personService = personService;

            this.personService.SubscriptionChanged += (sender, b) =>
            {
                this.LoggedOnAccount.Person.IsSubscribed = true;
                this.OnLoggedOnAccountChanged();
            };
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
        public async Task Authenticate(string email, string password)
        {
            var token = await this.authApi.Authenticate(new AuthenticateCommand
            {
                Email = email,
                Password = password
            });
            var myself = await this.personService.GetMyself();

            this.LoggedOnAccount = new AccountModel(token, myself);
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
        /// Restores an Authentication state based off of <see cref="account"/>.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <returns></returns>
        public async Task RestoreFromAccount(AccountModel account)
        {
            this.LoggedOnAccount = account;
            await this.authApi.Authenticate(account.Token);
            var myself = await this.personService.GetMyself();
            this.LoggedOnAccount.Person = myself;
        }

        /// <summary>
        /// Registers the user using the provided parameters.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        public async Task Register(string firstName, string lastName, string email, string password, string username)
        {
            var token = await this.authApi.Register(new CreateAccountCommand
            {
                FirstName = firstName,
                LastName = lastName,
                Password = password,
                Email = email,
                Username = username,
            });
            var myself = await this.personService.GetMyself();

            this.LoggedOnAccount = new AccountModel(token, myself);
        }
    }
}