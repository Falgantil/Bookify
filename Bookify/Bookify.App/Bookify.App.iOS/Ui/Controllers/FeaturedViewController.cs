using System;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Bookify.App.Core.ViewModels;
using Bookify.App.iOS.Ui.Controllers.Base;
using Bookify.App.iOS.Ui.DataSources;
using Bookify.App.iOS.Ui.Views;
using Bookify.Common.Models;
using Rope.Net.iOS;
using UIKit;

namespace Bookify.App.iOS.Ui.Controllers
{
    public partial class FeaturedViewController : ExtendedViewController<FeaturedViewModel>
    {
        private int booksCount;

        public FeaturedViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.tblContent.RowHeight = 100;
            this.tblContent.RegisterNibForCellReuse(BookTableCell.Nib, BookTableCell.Key);
            this.tblContent.Source = new FeaturedDataSource(this, this.ViewModel);

            this.ViewModel.Books.CollectionChanged += this.BooksCollectionChanged;
        }

        protected override void CreateBindings()
        {
            this.View.Bind(
                this.ViewModel.Books,
                collection => collection.IsLoading,
                (v, isLoading) => UIApplication.SharedApplication.NetworkActivityIndicatorVisible = isLoading);
        }

        public override async void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            await this.TryTask(async () => await this.ViewModel.Books.LoadMore());
        }

        private void BooksCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            this.tblContent.ReloadData();
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