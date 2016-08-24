using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

using Bookify.App.Core.Interfaces.Services;
using Bookify.App.Sdk.Exceptions;
using Bookify.Common.Models;
using Polly;

namespace Bookify.App.Core.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IAuthenticationService authService;

        public LoginViewModel(IAuthenticationService authService)
        {
            this.authService = authService;
        }

        public string Email { get; set; }

        public string Password { get; set; }

        public async Task Authenticate()
        {
            await this.TryTask(async () => await this.authService.Authenticate(this.Email, this.Password));
        }
    }
}
