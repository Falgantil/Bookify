using System.Threading.Tasks;

using Bookify.App.Core.Interfaces.Services;
using Bookify.App.Core.Services;

namespace Bookify.App.Core.ViewModels
{
    public class SidebarViewModel : BaseViewModel
    {
        /// <summary>
        /// The authentication service
        /// </summary>
        private readonly IAuthenticationService authenticationService;

        /// <summary>
        /// The person service
        /// </summary>
        private readonly IPersonService personService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SidebarViewModel" /> class.
        /// </summary>
        /// <param name="authenticationService">The authentication service.</param>
        /// <param name="personService">The person service.</param>
        public SidebarViewModel(IAuthenticationService authenticationService, IPersonService personService)
        {
            this.authenticationService = authenticationService;
            this.personService = personService;
            this.authenticationService.AuthChanged += this.AuthService_AuthChanged;
        }

        /// <summary>
        /// Authentications the service authentication changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="accountModel">The account model.</param>
        private void AuthService_AuthChanged(object sender, AccountModel accountModel)
        {
            this.OnPropertyChanged(nameof(this.Account));
            this.OnPropertyChanged(nameof(this.IsLoggedIn));
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public override void Dispose()
        {
            this.authenticationService.AuthChanged -= this.AuthService_AuthChanged;
            base.Dispose();
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

        /// <summary>
        /// Logouts this instance.
        /// </summary>
        /// <returns></returns>
        public async Task Logout()
        {
            await this.authenticationService.Deauthenticate();
        }

        /// <summary>
        /// Purchases the subscription.
        /// </summary>
        /// <returns></returns>
        public async Task PurchaseSubscription()
        {
            await this.personService.PurchaseSubscription();
        }
    }
}