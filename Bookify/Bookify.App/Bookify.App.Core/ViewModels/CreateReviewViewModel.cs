using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookify.App.Core.Interfaces.Services;
using Bookify.Common.Models;

namespace Bookify.App.Core.ViewModels
{
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

            return
                await
                    this.TryTask(async () => await this.service.CreateFeedback(this.book.Id, this.Rating, this.Message));
        }

        public IEnumerable<string> VerifyReview()
        {
            if (this.Rating <= 0) yield return "Anvig en vurdering fra 1 - 5! ";
            if (string.IsNullOrEmpty(this.Message)) yield return "Skriv en besked! ";
        }
    }
}