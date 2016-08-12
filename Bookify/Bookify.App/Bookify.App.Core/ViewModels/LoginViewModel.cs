﻿using System;
using System.Threading.Tasks;

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
            var result = await Policy.Handle<Exception>()
                .RetryAsync()
                .ExecuteAndCaptureAsync(op);
            if (result.Outcome == OutcomeType.Successful)
            {
                return result.Result;
            }

            return null;
        } 
    }
}