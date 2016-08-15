using System;
using System.Threading.Tasks;

using Bookify.App.Core.Interfaces.Services;
using Bookify.App.Core.Models;

namespace Bookify.App.Core.ViewModels
{
    public class BookSummaryViewModel : BaseViewModel
    {
        private IBasketService basketService;

        public BookSummaryViewModel(BookModel book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            this.Book = book;
        }

        public BookModel Book { get; }

        public bool OwnsBook => this.Book.OwnsBook;

        public bool Borrowable => this.Book.Borrowable;

        public async Task AddToBasket()
        {
            await this.basketService.AddToBasket(this.Book);
        }
    }
}