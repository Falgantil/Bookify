using System;
using System.Collections.Specialized;
using Bookify.App.Core.ViewModels;
using Bookify.App.iOS.Ui.Controllers.Base;
using Bookify.App.iOS.Ui.DataSources;
using Bookify.App.iOS.Ui.Views;
using Bookify.Common.Models;
using Rope.Net.iOS;
using UIKit;

namespace Bookify.App.iOS.Ui.Controllers
{
    public partial class MyBooksViewController : ExtendedViewController<MyBooksViewModel>
    {
        private bool hasRendered;

        public const string StoryboardIdentifier = "MyBooksViewController";

        public MyBooksViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.tblContent.RowHeight = 100;
            this.tblContent.RegisterNibForCellReuse(BookTableCell.Nib, BookTableCell.Key);
            this.tblContent.Source = new MyBooksDataSource(this, this.ViewModel);
        }

        public override async void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            this.ViewModel.Books.CollectionChanged += this.BooksCollectionChanged;

            if (this.hasRendered)
            {
                return;
            }

            this.hasRendered = true;

            await this.ViewModel.Books.LoadMore();
        }

        public override void ViewDidDisappear(bool animated)
        {
            this.ViewModel.Books.CollectionChanged -= this.BooksCollectionChanged;
            base.ViewDidDisappear(animated);
        }

        private void BooksCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.tblContent.ReloadData();
        }

        public override async void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            if (this.ViewModel.Books.Count > 0)
            {
                return;
            }

            await this.TryTask(async () => await this.ViewModel.Books.LoadMore());
        }

        protected override void CreateBindings()
        {
            this.View.Bind(
                this.ViewModel.Books,
                collection => collection.IsLoading,
                (v, isLoading) => UIApplication.SharedApplication.NetworkActivityIndicatorVisible = isLoading);
        }

        public async void CellTapped(BookDto model)
        {
            DetailedBookDto book;
            using (this.DialogService.Loading("Henter bog..."))
            {
                book =
                    await
                        this.TryTask(
                            async () => await this.ViewModel.GetBook(model.Id),
                            null,
                            null,
                            "Bogen kunne ikke hentes");
                if (book == null)
                {
                    return;
                }
            }
            var viewController = (BookOverviewViewController)this.Storyboard.InstantiateViewController(BookOverviewViewController.StoryboardIdentifier);
            viewController.Book = book;
            this.NavigationController.PushViewController(viewController, true);
        }
    }
}