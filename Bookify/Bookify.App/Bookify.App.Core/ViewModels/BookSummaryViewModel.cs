using Bookify.App.Core.Models;

namespace Bookify.App.Core.ViewModels
{
    public class BookSummaryViewModel : BaseViewModel
    {
        public BookModel Book { get; private set; }

        public BookSummaryViewModel(BookModel book)
        {
            this.Book = book;
        }
    }
}