// Bookify.App
// - Bookify.App.Core
// -- LoginViewModel.cs
// -------------------------------------------
// Author: Bjarke Søgaard <sogaardbjarke@gmail.com>

using System.Threading.Tasks;

using Bookify.App.Core.Interfaces.Services;

namespace Bookify.App.Core.ViewModels
{
    /// <summary>
    ///     The Login View Model, used when Authenticating
    /// </summary>
    /// <seealso cref="BaseViewModel" />
    public class LoginViewModel : BaseViewModel
    {
        /// <summary>
        ///     The authentication service
        /// </summary>
        private readonly IAuthenticationService authService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="LoginViewModel" /> class.
        /// </summary>
        /// <param name="authService">The authentication service.</param>
        public LoginViewModel(IAuthenticationService authService)
        {
            this.authService = authService;
        }

        /// <summary>
        ///     Gets or sets the email used when authenticating.
        /// </summary>
        /// <value>
        ///     The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        ///     Gets or sets the password used when authenticating.
        /// </summary>
        /// <value>
        ///     The password.
        /// </value>
        public string Password { get; set; }

        /// <summary>
        ///     Attempts to authenticate using the <see cref="Email" /> and <see cref="Password" />.
        /// </summary>
        /// <seealso cref="IAuthenticationService" />
        /// <returns></returns>
        public async Task Authenticate()
        {
            await this.TryTask(async () => await this.authService.Authenticate(this.Email, this.Password));
        }
    }
}