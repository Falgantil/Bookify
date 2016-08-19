using System.Collections.ObjectModel;
using System.Net;
using System.Threading.Tasks;
using Bookify.App.Core.Interfaces.Services;
using Bookify.Common.Models;
using Polly;

namespace Bookify.App.Core.ViewModels
{
    public class SearchViewModel : BaseViewModel
    {
        private readonly IBookService bookService;

        public SearchViewModel(IBookService bookService)
        {
            this.bookService = bookService;
        }

        public ObservableCollection<BookDto> Filtered { get; } = new ObservableCollection<BookDto>();

        public string SearchText { get; set; }

        public GenreDto Genre { get; set; }

        private int delays;

        public void RefreshContent()
        {
            this.OnSearchTextChanged();
        } 

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

        public async Task<DetailedBookDto> GetBook(int id)
        {
            var result = await
                         Policy.Handle<WebException>()
                             .RetryAsync()
                             .ExecuteAndCaptureAsync(async () => await this.bookService.GetBook(id));
            if (result.Outcome == OutcomeType.Failure)
            {
                return null;
            }
            return result.Result;
        }
    }
}