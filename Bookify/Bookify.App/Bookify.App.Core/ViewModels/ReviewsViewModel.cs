using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Bookify.App.Core.Collections;
using Bookify.App.Core.Interfaces.Services;
using Bookify.App.Core.Models;
using Bookify.App.Core.Services;
using Bookify.Common.Filter;
using Bookify.Common.Models;
using Polly;

namespace Bookify.App.Core.ViewModels
{
    public class ReviewsViewModel : BaseViewModel
    {
        public ReviewsViewModel(DetailedBookDto book, IFeedbackService feedbackService)
        {
            var feedbackFilter = new FeedbackFilter
            {
                BookId = book.Id
            };
            this.Reviews = new ObservableServiceCollection<FeedbackDto, FeedbackFilter, IFeedbackService>(feedbackService, feedbackFilter);
        }

        public ObservableServiceCollection<FeedbackDto, FeedbackFilter, IFeedbackService> Reviews { get; }
    }
}