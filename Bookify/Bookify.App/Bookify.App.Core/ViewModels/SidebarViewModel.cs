using System.Threading.Tasks;

using Bookify.App.Core.Interfaces.Services;
using Bookify.App.Core.Services;
using Bookify.Common.Models;

namespace Bookify.App.Core.ViewModels
{
    public class SidebarViewModel : BaseViewModel
    {
        private readonly IAuthenticationService authenticationService;

        public SidebarViewModel(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
            this.authenticationService.AuthChanged += this.AuthChanged;
        }

        /// <summary>
        /// Gets the account. Will be null if the user is not logged in.
        /// </summary>
        /// <value>
        /// The account.
        /// </value>
        public AccountModel Account { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is logged in.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is logged in; otherwise, <c>false</c>.
        /// </value>
        public bool IsLoggedIn => this.Account != null;

        /// <summary>
        /// Invoked when the authentication state changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="person">The account model.</param>
        private void AuthChanged(object sender, AccountModel person)
        {
            this.Account = person;
        }

        public async Task Logout()
        {
            await this.authenticationService.Deauthenticate();
        }
    }
}