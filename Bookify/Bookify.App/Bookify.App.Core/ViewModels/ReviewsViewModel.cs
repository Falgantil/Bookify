using Bookify.App.Core.Collections;
using Bookify.App.Core.Interfaces.Services;
using Bookify.Common.Filter;
using Bookify.Common.Models;

namespace Bookify.App.Core.ViewModels
{
    public class ReviewsViewModel : BaseViewModel
    {
        private readonly IAuthenticationService authService;

        public ReviewsViewModel(DetailedBookDto book, IFeedbackService feedbackService, IAuthenticationService authService)
        {
            this.authService = authService;
            var feedbackFilter = new FeedbackFilter
            {
                BookId = book.Id
            };
            this.Reviews = new ObservableServiceCollection<BookFeedbackDto, FeedbackFilter, IFeedbackService>(feedbackService, feedbackFilter);
        }

        public ObservableServiceCollection<BookFeedbackDto, FeedbackFilter, IFeedbackService> Reviews { get; }

        public bool IsLoggedIn => this.authService.LoggedOnAccount != null;
    }
}