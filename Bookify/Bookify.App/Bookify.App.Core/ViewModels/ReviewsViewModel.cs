using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Bookify.App.Core.Interfaces.Services;
using Bookify.App.Core.Models;
using Bookify.Common.Models;
using Polly;

namespace Bookify.App.Core.ViewModels
{
    public class ReviewsViewModel : BaseViewModel
    {
        private readonly IReviewService reviewService;
        private readonly DetailedBookDto book;

        public ReviewsViewModel(DetailedBookDto book, IReviewService reviewService)
        {
            this.book = book;
            this.reviewService = reviewService;
        }

        public ObservableCollection<ReviewModel> Reviews { get; } = new ObservableCollection<ReviewModel>();

        public async Task LoadReviews()
        {
            var result = await this.reviewService.GetReviews(this.book);

            var models = result.ToArray();
            foreach (var m in models)
            {
                this.Reviews.Add(m);
            }
        }
    }
}