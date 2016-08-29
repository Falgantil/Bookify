using System.Threading.Tasks;
using Bookify.App.Core.Interfaces;
using Bookify.App.Core.Interfaces.Services;

namespace Bookify.App.Core.Services
{
    /// <summary>
    /// The Config implementation.
    /// </summary>
    /// <seealso cref="IConfig" />
    public class Config : IConfig
    {
        /// <summary>
        /// The authentication service
        /// </summary>
        private readonly IAuthenticationService authService;

        /// <summary>
        /// The account
        /// </summary>
        private readonly ICachingRegion<AccountModel> account;

        /// <summary>
        /// Initializes a new instance of the <see cref="Config"/> class.
        /// </summary>
        /// <param name="cachingFactory">The caching factory.</param>
        /// <param name="authService">The authentication service.</param>
        public Config(ICachingRegionFactory cachingFactory, IAuthenticationService authService)
        {
            this.authService = authService;
            this.account = cachingFactory.CreateRegion<AccountModel>("config");

            this.authService.AuthChanged += this.SaveAuthState;
        }

        /// <summary>
        /// Saves the state of the authentication.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private async void SaveAuthState(object sender, AccountModel e)
        {
            await this.account.UpdateItem(e);
        }

        /// <summary>
        /// Loads the account.
        /// </summary>
        /// <returns></returns>
        public async Task<AccountModel> LoadAccount()
        {
            return await this.account.GetItem();
        }

        /// <summary>
        /// Restores the account.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> RestoreAccount()
        {
            var account = await this.LoadAccount();
            if (account == null)
            {
                return false;
            }

            this.authService.RestoreFromAccount(account);

            return true;
        }
    }
}