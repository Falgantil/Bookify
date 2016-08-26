using System;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

using Bookify.App.Core.Interfaces.Services;
using Bookify.Common.Models;

namespace Bookify.App.Core.ViewModels
{
    public class BookSummaryViewModel : BaseViewModel
    {
        private readonly IBooksService booksService;
        private readonly IShoppingCartService shoppingCartService;
        private readonly IAuthenticationService authService;

        public BookSummaryViewModel(DetailedBookDto book, IBooksService booksService, IShoppingCartService shoppingCartService, IAuthenticationService authService)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            this.Book = book;
            this.booksService = booksService;
            this.shoppingCartService = shoppingCartService;
            this.authService = authService;
            this.booksService.MyBooks.CollectionChanged += this.MyBooks_CollectionChanged;
        }

        public override void Dispose()
        {
            base.Dispose();

            this.booksService.MyBooks.CollectionChanged -= this.MyBooks_CollectionChanged;
        }

        private void MyBooks_CollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            this.OnPropertyChanged(nameof(this.OwnsBook));
        }

        public DetailedBookDto Book { get; }

        public bool IsLoggedIn => this.authService.LoggedOnAccount != null;

        public bool OwnsBook => this.booksService.MyBooks.Any(k => k.Id == this.Book.Id);

        public async Task AddToCart()
        {
            await this.shoppingCartService.AddToCart(this.Book);
        }

        public async Task BorrowBook()
        {

        }

        public async Task<byte[]> DownloadBook()
        {
            return await this.booksService.DownloadBook(this.Book.Id);
        }
    }
}