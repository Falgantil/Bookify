using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookify.App.Core.Interfaces.Services;

namespace Bookify.App.Core.ViewModels
{
    public class RegistrationViewModel : BaseViewModel
    {
        /// <summary>
        /// The service
        /// </summary>
        private readonly IAuthenticationService service;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationViewModel"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        public RegistrationViewModel(IAuthenticationService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the password repeat.
        /// </summary>
        /// <value>
        /// The password repeat.
        /// </value>
        public string PasswordRepeat { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username { get; set; }

        /// <summary>
        /// Registers the user with the provided credentials. The user is automatically signed in if the account was successfully created.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Invalid input!</exception>
        public async Task Register()
        {
            if (this.VerifyData().Any())
            {
                throw new ArgumentException("Invalid input!");
            }
            Func<Task> op = async () =>
                await this.service.Register(this.FirstName, this.LastName, this.Email, this.Password, this.Username);
            await this.TryTask(op);
        }
        
        /// <summary>
        /// Verifies that all properties has a valid value.
        /// If not, returns error messages.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> VerifyData()
        {
            if (string.IsNullOrEmpty(this.FirstName))
                yield return nameof(this.FirstName);
            if (string.IsNullOrEmpty(this.LastName))
                yield return nameof(this.FirstName);
            if (string.IsNullOrEmpty(this.Email))
                yield return nameof(this.FirstName);
            if (string.IsNullOrEmpty(this.Password))
                yield return nameof(this.FirstName);
            if (string.IsNullOrEmpty(this.PasswordRepeat))
                yield return nameof(this.FirstName);
            if (string.IsNullOrEmpty(this.Username))
                yield return nameof(this.FirstName);

            if (!this.Password.Equals(this.PasswordRepeat))
                yield return $"{nameof(this.Password)} - {nameof(this.PasswordRepeat)}";
        }
    }
}