using System.Threading.Tasks;
using Bookify.App.Core.Services;

namespace Bookify.App.Core.Interfaces
{
    public interface IConfig
    {
        /// <summary>
        /// Loads the account.
        /// </summary>
        /// <returns></returns>
        Task<AccountModel> LoadAccount();

        /// <summary>
        /// Restores the account.
        /// </summary>
        /// <returns></returns>
        Task<bool> RestoreAccount();
    }
}