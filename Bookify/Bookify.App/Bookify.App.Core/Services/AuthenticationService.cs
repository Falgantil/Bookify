using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Bookify.App.Core.Annotations;
using Bookify.App.Core.Interfaces.Services;
using Bookify.App.Sdk.Implementations;
using Bookify.App.Sdk.Interfaces;
using Bookify.Common.Commands.Auth;
using Bookify.Common.Models;

namespace Bookify.App.Core.Services
{
    public class AuthenticationService : IAuthenticationService, INotifyPropertyChanged
    {
        private AuthenticationApi api;

        private IPersonApi personApi;

        public AuthenticationService(AuthenticationApi api)
        {
            this.api = api;
        }

        /// <summary>
        /// Occurs when the authentication state changed (Logged in or out).
        /// </summary>
        public event EventHandler<PersonDto> AuthChanged;

        /// <summary>
        /// Gets the logged on account. Returns null if not currently logged on.
        /// </summary>
        /// <value>
        /// The logged on account.
        /// </value>
        public PersonDto LoggedOnAccount { get; private set; }

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
            var token = await this.api.Authenticate(new AuthenticateCommand
            {
                Email = email,
                Password = password
            });
            var myself = await this.personApi.GetMyself();
            
            this.LoggedOnAccount = myself;
            return myself;
        }

        /// <summary>
        /// Deauthenticates the user.
        /// </summary>
        /// <returns></returns>
        public async Task Deauthenticate()
        {
            this.LoggedOnAccount = null;
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}