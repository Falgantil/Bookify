using System;
using System.Threading.Tasks;

using Bookify.App.Core.Interfaces.Services;
using Bookify.Common.Models;

namespace Bookify.App.Core.Services
{
    public interface IConfig
    {
        Task<AccountModel> LoadAccount();

        Task<bool> RestoreAccount();
    }

    public class Config : IConfig
    {
        private readonly ICachingRegionFactory cachingFactory;

        private readonly IAuthenticationService authService;

        private readonly ICachingRegion<AccountModel> getAccount;

        public Config(ICachingRegionFactory cachingFactory, IAuthenticationService authService)
        {
            this.cachingFactory = cachingFactory;
            this.authService = authService;
            this.getAccount = this.cachingFactory.CreateRegion<AccountModel>("config.dat");

            this.authService.AuthChanged += this.SaveAuthState;
        }

        private async void SaveAuthState(object sender, AccountModel e)
        {
            await this.getAccount.UpdateItem(e);
        }

        public async Task<AccountModel> LoadAccount()
        {
            return await this.getAccount.GetItem();
        }

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