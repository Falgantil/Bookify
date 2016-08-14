using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Bookify.App.Core.Exceptions;
using Bookify.App.Core.Helpers;
using Bookify.App.Core.Interfaces.Services;
using Bookify.App.Core.Models;

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

        public string Username { get; set; }

        public string Password { get; set; }

        public async Task<AccountModel> Authenticate()
        {
            Func<Task<AccountModel>> op = async () => await this.authService.Authenticate(this.Username, this.Password);
            var result = await Policy.Handle<WebException>()
                .Or<HttpResponseException>()
                .RetryAsync()
                .ExecuteAndCaptureAsync(op);
            if (result.Outcome == OutcomeType.Failure)
            {
                throw result.FinalException;
            }

            return result.Result;
        }
    }
}
