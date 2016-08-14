using System;
using System.Threading.Tasks;

using Bookify.App.Core.Models;

namespace Bookify.App.Core.Interfaces.Services
{
    /// <summary>
    /// The <see cref="IAuthenticationService"/> interface
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Occurs when the authentication state changed (Logged in or out).
        /// </summary>
        event EventHandler<AccountModel> AuthChanged;

        /// <summary>
        /// Gets the logged on account. Returns null if not currently logged on.
        /// </summary>
        /// <value>
        /// The logged on account.
        /// </value>
        AccountModel LoggedOnAccount { get; }

        /// <summary>
        /// Authenticates the user using the provided <see cref="username"/> and <see cref="password"/>.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        Task<AccountModel> Authenticate(string username, string password);
    }
}