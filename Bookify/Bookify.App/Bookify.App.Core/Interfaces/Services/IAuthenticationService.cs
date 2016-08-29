using System;
using System.Threading.Tasks;

using Bookify.App.Core.Services;

namespace Bookify.App.Core.Interfaces.Services
{
    /// <summary>
    /// The Authentication Service interface
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
        /// <param name="emailame">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        Task Authenticate(string email, string password);

        /// <summary>
        /// Deauthenticates the user.
        /// </summary>
        /// <returns></returns>
        Task Deauthenticate();

        /// <summary>
        /// Restores from account.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <returns></returns>
        Task RestoreFromAccount(AccountModel account);

        /// <summary>
        /// Registers the user using the provided parameters.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        Task Register(string firstName, string lastName, string email, string password, string username);
    }
}