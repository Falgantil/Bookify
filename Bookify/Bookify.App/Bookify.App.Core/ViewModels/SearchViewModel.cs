using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Bookify.App.Core.Interfaces.Services;
using Bookify.App.Core.Models;
using Bookify.Models;
using Book = Bookify.Models.Book;

namespace Bookify.App.Core.ViewModels
{
    public class SearchViewModel : BaseViewModel
    {
        private readonly IBookService bookService;

        public SearchViewModel(IBookService bookService)
        {
            this.bookService = bookService;
            this.OnSearchTextChanged();
        }

        public ObservableCollection<Book> Filtered { get; set; }

        public string SearchText { get; set; }

        private int delays;

        private async void OnSearchTextChanged()
        {
            this.delays++;

            await Task.Delay(1000);

            this.delays--;

            if (this.delays > 0)
            {
                return;
            }

            await this.PerformSearch();
        }

        private async Task PerformSearch()
        {
            var books = await this.bookService.GetBooks(0, 10, this.SearchText);

            this.Filtered.Clear();
            foreach (var book in books)
            {
                this.Filtered.Add(book);
            }
        }
    }
}