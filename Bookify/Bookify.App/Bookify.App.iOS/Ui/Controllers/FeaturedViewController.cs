using System;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Bookify.App.Core.ViewModels;
using Bookify.App.iOS.Ui.Controllers.Base;
using Bookify.App.iOS.Ui.DataSources;
using Bookify.App.iOS.Ui.Views;
using Bookify.Common.Models;
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
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            this.LoadMore();
        }

        public async Task LoadMore()
        {
            UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
            var books = await this.ViewModel.LoadBooks(this.booksCount, 10);
            UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
            if (books == null)
            {
                return;
            }
            this.booksCount += books.Count();
        }

        private void BooksCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            this.tblContent.ReloadData();
        }
        
        public async void CellTapped(BookDto model)
        {
            BookDto book;
            using (this.DialogService.Loading("Henter bog..."))
            {
                book = await this.ViewModel.GetBook(model.Id);
                if (book == null)
                {
                    this.DialogService.Alert("Bogen kunne ikke hentes", "Fejl");
                    return;
                }
            }
            var viewController = (BookOverviewViewController)this.Storyboard.InstantiateViewController("ShowBook");
            viewController.Book = book;
            this.NavigationController.PushViewController(viewController, true);
        }
    }
}