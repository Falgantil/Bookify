using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookify.App.Core.Interfaces.Services;

namespace Bookify.App.Core.ViewModels
{
    public class RegistrationViewModel : BaseViewModel
    {
        private readonly IAuthenticationService service;

        public RegistrationViewModel(IAuthenticationService service)
        {
            this.service = service;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string PasswordRepeat { get; set; }

        public string Username { get; set; }

        public async Task Register()
        {
            if (!this.ValidInput)
            {
                throw new ArgumentException("Invalid input!");
            }
            Func<Task> op = async () =>
                await this.service.Register(this.FirstName, this.LastName, this.Email, this.Password, this.Username);
            await this.TryTask(op);
        }

        public bool ValidInput => !this.GetInvalidFields().Any();

        public IEnumerable<string> GetInvalidFields()
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