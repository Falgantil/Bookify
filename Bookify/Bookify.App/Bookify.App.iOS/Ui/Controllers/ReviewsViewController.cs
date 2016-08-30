using System;
using System.Collections.Specialized;
using Bookify.App.Core.Initialization;
using Bookify.App.Core.ViewModels;
using Bookify.App.iOS.Initialization;
using Bookify.App.iOS.Ui.Controllers.Base;
using Bookify.App.iOS.Ui.DataSources;
using Bookify.Common.Models;
using Rope.Net.iOS;
using UIKit;

namespace Bookify.App.iOS.Ui.Controllers
{
    public partial class ReviewsViewController : ExtendedViewController<ReviewsViewModel>
    {
        private UIBarButtonItem btnAddReview;
        private bool rendered;

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

            if (this.ViewModel.IsLoggedIn)
            {
                this.btnAddReview = new UIBarButtonItem(UIBarButtonSystemItem.Add, this.BtnAddReview_Click);
            }
        }

        private void BtnAddReview_Click(object sender, EventArgs e)
        {
            var storyboard = Storyboards.Storyboard.Main;
            var vc = (CreateReviewViewController)storyboard.InstantiateViewController(CreateReviewViewController.StoryboardIdentifier);
            vc.Book = this.Book;
            vc.CreatedReview += (o, dto) => this.ViewModel.Reviews.Insert(0, dto);
            this.NavigationController.PushViewController(vc, true);
        }

        public override async void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            // If not logged in, the button will be null. Calling SetRightBarButtonItem
            // with null, removes any other button. Which is what we want.
            this.ParentViewController.NavigationItem.SetRightBarButtonItem(this.btnAddReview, false);

            if (this.rendered)
            {
                return;
            }
            this.rendered = true;

            this.ViewModel.Reviews.AddRange(this.Book.Feedback);
            await this.TryTask(async () => await this.ViewModel.Reviews.LoadMore());
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