using Bookify.App.Core.Collections;
using Bookify.App.Core.Interfaces.Services;
using Bookify.Common.Filter;
using Bookify.Common.Models;

namespace Bookify.App.Core.ViewModels
{
    public class ReviewsViewModel : BaseViewModel
    {
        /// <summary>
        /// The authentication service
        /// </summary>
        private readonly IAuthenticationService authService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReviewsViewModel" /> class.
        /// </summary>
        /// <param name="book">The book.</param>
        /// <param name="feedbackService">The feedback service.</param>
        /// <param name="authService">The authentication service.</param>
        public ReviewsViewModel(DetailedBookDto book, IFeedbackService feedbackService, IAuthenticationService authService)
        {
            this.authService = authService;
            var feedbackFilter = new FeedbackFilter
            {
                BookId = book.Id
            };
            this.Reviews = new ObservableServiceCollection<BookFeedbackDto, FeedbackFilter, IFeedbackService>(feedbackService, feedbackFilter);
        }

        /// <summary>
        /// Gets the reviews.
        /// </summary>
        /// <value>
        /// The reviews.
        /// </value>
        public ObservableServiceCollection<BookFeedbackDto, FeedbackFilter, IFeedbackService> Reviews { get; }

        /// <summary>
        /// Gets a value indicating whether the user is logged in.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is logged in; otherwise, <c>false</c>.
        /// </value>
        public bool IsLoggedIn => this.authService.LoggedOnAccount != null;
    }
}