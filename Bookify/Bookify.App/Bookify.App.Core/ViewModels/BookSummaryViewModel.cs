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
        /// <summary>
        /// The books service
        /// </summary>
        private readonly IBooksService booksService;

        /// <summary>
        /// The shopping cart service
        /// </summary>
        private readonly IShoppingCartService shoppingCartService;

        /// <summary>
        /// The authentication service
        /// </summary>
        private readonly IAuthenticationService authService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookSummaryViewModel"/> class.
        /// </summary>
        /// <param name="book">The book.</param>
        /// <param name="booksService">The books service.</param>
        /// <param name="shoppingCartService">The shopping cart service.</param>
        /// <param name="authService">The authentication service.</param>
        /// <exception cref="ArgumentNullException"></exception>
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

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();

            this.booksService.MyBooks.CollectionChanged -= this.MyBooks_CollectionChanged;
        }

        /// <summary>
        /// Handles the CollectionChanged event of the MyBooks control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="notifyCollectionChangedEventArgs">The <see cref="NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
        private void MyBooks_CollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            this.OnPropertyChanged(nameof(this.OwnsBook));
        }

        /// <summary>
        /// Gets the book.
        /// </summary>
        /// <value>
        /// The book.
        /// </value>
        public DetailedBookDto Book { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is logged in.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is logged in; otherwise, <c>false</c>.
        /// </value>
        public bool IsLoggedIn => this.authService.LoggedOnAccount != null;

        /// <summary>
        /// Gets a value indicating whether the user owns this book.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [owns book]; otherwise, <c>false</c>.
        /// </value>
        public bool OwnsBook => this.booksService.MyBooks.Any(k => k.Id == this.Book.Id);

        /// <summary>
        /// Adds the book to the cart.
        /// </summary>
        /// <returns></returns>
        public async Task AddToCart()
        {
            await this.shoppingCartService.AddToCart(this.Book);
        }

        /// <summary>
        /// Borrows the book.
        /// </summary>
        /// <returns></returns>
        public async Task BorrowBook()
        {

        }

        /// <summary>
        /// Downloads the book as a byte array.
        /// </summary>
        /// <returns></returns>
        public async Task<byte[]> DownloadBook()
        {
            return await this.booksService.DownloadBook(this.Book.Id);
        }
    }
}