using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Bookify.App.Core.Interfaces.Services;
using Bookify.App.Core.Services;
using Bookify.App.Sdk.Exceptions;
using Bookify.App.Sdk.Interfaces;
using Bookify.Common.Models;
using Polly;

namespace Bookify.App.Core.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class CreateReviewViewModel : BaseViewModel
    {
        private readonly DetailedBookDto book;

        private readonly IAuthenticationService authService;
        private readonly IFeedbackService service;

        public CreateReviewViewModel(DetailedBookDto book, IAuthenticationService authService, IFeedbackService service)
        {
            this.book = book;
            this.authService = authService;
            this.service = service;
            if (this.authService.LoggedOnAccount == null)
            {
                throw new ArgumentNullException(nameof(this.authService.LoggedOnAccount), "Missing logged on user!");
            }
        }

        public PersonDto Person => this.authService.LoggedOnAccount.Person;

        public int Rating { get; set; }

        public string Message { get; set; }

        public async Task<BookFeedbackDto> CreateReview()
        {
            if (this.VerifyReview().Any())
            {
                throw new InvalidOperationException("Missing rating!");
            }
            
            Func<Task<BookFeedbackDto>> op = async () => await this.service.CreateFeedback(this.book.Id, this.Rating, this.Message);
            var result = await Policy
                .Handle<WebException>()
                .Or<HttpResponseException>()
                .RetryAsync()
                .ExecuteAsync(op);
            return result;
        }

        public IEnumerable<string> VerifyReview()
        {
            if (this.Rating <= 0) yield return "Anvig en vurdering fra 1 - 5! ";
            if (string.IsNullOrEmpty(this.Message)) yield return "Skriv en besked! ";
        }
    }
}