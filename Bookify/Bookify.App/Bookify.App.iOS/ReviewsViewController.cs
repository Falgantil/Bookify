using Foundation;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Bookify.App.Core.Initialization;
using Bookify.App.Core.Models;
using Bookify.App.Core.ViewModels;
using Bookify.App.iOS.Initialization;
using Bookify.App.iOS.Ui.Controllers.Base;
using UIKit;

namespace Bookify.App.iOS
{
    public partial class ReviewsViewController : ExtendedViewController<ReviewsViewModel>
    {
        public ReviewsViewController (IntPtr handle) : base (handle)
        {
        }

        public BookModel Book { get; set; }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.tblContent.Source = new ReviewsDataSource(this.ViewModel);
            this.tblContent.RowHeight = UITableView.AutomaticDimension;
            this.tblContent.EstimatedRowHeight = 170;
            this.ViewModel.Reviews.CollectionChanged += this.ReviewsCollectionChanged;
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            this.LoadReviews();
        }

        private async void LoadReviews()
        {
            UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
            await this.ViewModel.LoadReviews();
            UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
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
            
        }
    }

    public class ReviewsDataSource : UITableViewSource
    {
        private readonly ReviewsViewModel viewModel;

        public ReviewsDataSource(ReviewsViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            tableView.DeselectRow(indexPath, true);
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            return ReviewTableCell.CreateCell(tableView, indexPath, this.viewModel.Reviews[indexPath.Row]);
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            if (section == 0)
            {
                return this.viewModel.Reviews.Count;
            }
            return 0;
        }
    }
}