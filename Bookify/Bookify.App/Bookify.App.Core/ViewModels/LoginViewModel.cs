using System;
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

        public async Task<PersonDto> Authenticate()
        {
            Func<Task<PersonDto>> op = async () => await this.authService.Authenticate(this.Email, this.Password);
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
