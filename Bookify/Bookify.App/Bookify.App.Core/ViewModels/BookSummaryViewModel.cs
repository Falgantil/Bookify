using System;
using System.Threading.Tasks;

using Bookify.App.Core.Interfaces.Services;
using Bookify.Common.Models;

namespace Bookify.App.Core.ViewModels
{
    public class BookSummaryViewModel : BaseViewModel
    {
        private readonly IShoppingCartService shoppingCartService;

        public BookSummaryViewModel(BookDto book, IShoppingCartService shoppingCartService)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            this.Book = book;
            this.shoppingCartService = shoppingCartService;
        }

        public BookDto Book { get; }

        //public bool OwnsBook => this.Book.OwnsBook;

        //public bool Borrowable => this.Book.Borrowable;

        public async Task AddToBasket()
        {
            await this.shoppingCartService.AddToBasket(this.Book);
        }
    }
}