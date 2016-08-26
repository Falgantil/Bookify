using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Bookify.App.Core.Interfaces.Services;
using Bookify.App.Core.Services;
using Bookify.Common.Models;
using eBdb.EpubReader;

namespace Bookify.App.Core.ViewModels
{
    public class BookSummaryViewModel : BaseViewModel
    {
        private readonly IBooksService booksService;
        private readonly IShoppingCartService shoppingCartService;
        private readonly IAuthenticationService authService;

        public BookSummaryViewModel(BookDto book, IBooksService booksService, IShoppingCartService shoppingCartService, IAuthenticationService authService)
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

        public BookDto Book { get; }

        public bool IsLoggedIn => this.authService.LoggedOnAccount != null;

        public bool OwnsBook => this.booksService.MyBooks.Any(k => k.Id == this.Book.Id);

        public async Task AddToCart()
        {
            await this.shoppingCartService.AddToCart(this.Book);
        }

        public async Task BorrowBook()
        {

        }

        public async Task<string> DownloadBook()
        {
            var downloadBook = await this.booksService.DownloadBook(this.Book.Id);
            var epub = new Epub(new MemoryStream(downloadBook));
            return await Task.Run(() => epub.GetContentAsHtml());
        }
    }
}