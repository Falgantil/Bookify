using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Bookify.App.Core.Interfaces.Services;
using Bookify.App.Core.Models;
using Bookify.Models;
using Polly;

namespace Bookify.App.Core.ViewModels
{
    public class ReviewsViewModel : BaseViewModel
    {
        private readonly IReviewService reviewService;
        private readonly Book book;

        public ReviewsViewModel(Book book, IReviewService reviewService)
        {
            this.book = book;
            this.reviewService = reviewService;
        }

        public ObservableCollection<ReviewModel> Reviews { get; } = new ObservableCollection<ReviewModel>();

        public async Task<IEnumerable<ReviewModel>> LoadReviews()
        {
            var result = await
                         Policy.Handle<WebException>()
                             .RetryAsync()
                             .ExecuteAndCaptureAsync(async () => await this.reviewService.GetReviews(this.book.Id));
            if (result.Outcome == OutcomeType.Failure)
            {
                return null;
            }

            var models = result.Result.ToArray();
            foreach (var m in models)
            {
                this.Reviews.Add(m);
            }

            return models;
        }
    }
}