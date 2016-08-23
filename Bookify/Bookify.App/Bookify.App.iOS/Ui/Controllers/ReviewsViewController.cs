using System;
using System.Collections.Specialized;
using System.Threading.Tasks;
using Bookify.App.Core.Initialization;
using Bookify.App.Core.ViewModels;
using Bookify.App.iOS.Initialization;
using Bookify.App.iOS.Ui.Controllers.Base;
using Bookify.App.iOS.Ui.DataSources;
using Bookify.Common.Exceptions;
using Bookify.Common.Models;
using Rope.Net.iOS;
using UIKit;

namespace Bookify.App.iOS.Ui.Controllers
{
    public partial class ReviewsViewController : ExtendedViewController<ReviewsViewModel>
    {
        public ReviewsViewController(IntPtr handle) : base(handle)
        {
        }

        public DetailedBookDto Book { get; set; }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.tblContent.Source = new ReviewsDataSource(this.ViewModel);
            this.tblContent.RowHeight = UITableView.AutomaticDimension;
            this.tblContent.EstimatedRowHeight = 170;
            this.ViewModel.Reviews.CollectionChanged += this.ReviewsCollectionChanged;
        }

        public override async void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            await this.ViewModel.Reviews.LoadMore();
        }

        private void ReviewsCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            this.tblContent.ReloadData();
        }

        protected override ReviewsViewModel CreateViewModel()
        {
            return AppDelegate.Root.Resolve<ReviewsViewModel>(new Parameter("book", this.Book));
        }

        protected override void CreateBindings()
        {
            this.View.Bind(
                this.ViewModel.Reviews,
                collection => collection.IsLoading,
                (view, isLoading) => UIApplication.SharedApplication.NetworkActivityIndicatorVisible = isLoading);
        }
    }
}