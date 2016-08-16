using System;
using System.Threading.Tasks;
using Bookify.App.Core.Interfaces.Services;
using Bookify.Models;

namespace Bookify.App.Core.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        /// <summary>
        /// Occurs when the authentication state changed (Logged in or out).
        /// </summary>
        public event EventHandler<Person> AuthChanged;

        /// <summary>
        /// Gets the logged on account. Returns null if not currently logged on.
        /// </summary>
        /// <value>
        /// The logged on account.
        /// </value>
        public Person LoggedOnAccount { get; private set; }

        /// <summary>
        /// Called when <see cref="LoggedOnAccount"/> has changed.
        /// </summary>
        private void OnLoggedOnAccountChanged()
        {
            this.AuthChanged?.Invoke(this, this.LoggedOnAccount);
        }

        /// <summary>
        /// Authenticates the user using the provided <see cref="username" /> and <see cref="password" />.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public async Task<Person> Authenticate(string username, string password)
        {
            await Task.Delay(1500);

            var person = new Person
            {
                Firstname = "Bjarke",
                Lastname = "Søgaard"
            };
            this.LoggedOnAccount = person;
            return person;
        }

        /// <summary>
        /// Deauthenticates the user.
        /// </summary>
        /// <returns></returns>
        public async Task Deauthenticate()
        {
            this.LoggedOnAccount = null;
        }
    }
}