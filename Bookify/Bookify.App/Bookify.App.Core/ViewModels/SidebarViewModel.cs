using System.Threading.Tasks;

using Bookify.App.Core.Interfaces.Services;
using Bookify.App.Core.Services;

namespace Bookify.App.Core.ViewModels
{
    public class SidebarViewModel : BaseViewModel
    {
        private readonly IAuthenticationService authenticationService;

        public SidebarViewModel(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
            this.authenticationService.AuthChanged += (sender, model) =>
            {
                this.OnPropertyChanged(nameof(this.Account));
                this.OnPropertyChanged(nameof(this.IsLoggedIn));
            };
        }

        /// <summary>
        /// Gets the account. Will be null if the user is not logged in.
        /// </summary>
        /// <value>
        /// The account.
        /// </value>
        public AccountModel Account => this.authenticationService.LoggedOnAccount;

        /// <summary>
        /// Gets a value indicating whether this instance is logged in.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is logged in; otherwise, <c>false</c>.
        /// </value>
        public bool IsLoggedIn => this.Account != null;
        
        public async Task Logout()
        {
            await this.authenticationService.Deauthenticate();
        }
    }
}