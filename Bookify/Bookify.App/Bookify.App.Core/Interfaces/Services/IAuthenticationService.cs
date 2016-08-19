using System;
using System.Threading.Tasks;
using Bookify.Common.Models;

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
        event EventHandler<PersonDto> AuthChanged;

        /// <summary>
        /// Gets the logged on account. Returns null if not currently logged on.
        /// </summary>
        /// <value>
        /// The logged on account.
        /// </value>
        PersonDto LoggedOnAccount { get; }

        /// <summary>
        /// Authenticates the user using the provided <see cref="username"/> and <see cref="password"/>.
        /// </summary>
        /// <param name="emailame">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        Task<PersonDto> Authenticate(string email, string password);

        /// <summary>
        /// Deauthenticates the user.
        /// </summary>
        /// <returns></returns>
        Task Deauthenticate();

    }
}