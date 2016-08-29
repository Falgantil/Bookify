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
        /// <summary>
        /// The book
        /// </summary>
        private readonly DetailedBookDto book;

        /// <summary>
        /// The authentication service
        /// </summary>
        private readonly IAuthenticationService authService;

        /// <summary>
        /// The service
        /// </summary>
        private readonly IFeedbackService service;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateReviewViewModel"/> class.
        /// </summary>
        /// <param name="book">The book.</param>
        /// <param name="authService">The authentication service.</param>
        /// <param name="service">The service.</param>
        /// <exception cref="ArgumentNullException">Missing logged on user!</exception>
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

        /// <summary>
        /// Gets the person.
        /// </summary>
        /// <value>
        /// The person.
        /// </value>
        public PersonDto Person => this.authService.LoggedOnAccount.Person;

        /// <summary>
        /// Gets or sets the rating.
        /// </summary>
        /// <value>
        /// The rating.
        /// </value>
        public int Rating { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Creates the review.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">Missing rating!</exception>
        public async Task<BookFeedbackDto> CreateReview()
        {
            if (this.VerifyData().Any())
            {
                throw new InvalidOperationException("Missing rating!");
            }

            return
                await
                    this.TryTask(async () => await this.service.CreateFeedback(this.book.Id, this.Rating, this.Message));
        }

        /// <summary>
        /// Verifies that all properties has a valid value.
        /// If not, returns error messages.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> VerifyData()
        {
            if (this.Rating <= 0) yield return "Anvig en vurdering fra 1 - 5! ";
            if (string.IsNullOrEmpty(this.Message)) yield return "Skriv en besked! ";
        }
    }
}