using System.Collections.ObjectModel;
using System.Net;
using System.Threading.Tasks;

using Bookify.App.Core.Collections;
using Bookify.App.Core.Interfaces.Services;
using Bookify.App.Core.Services;
using Bookify.App.Sdk.Interfaces;
using Bookify.Common.Filter;
using Bookify.Common.Models;
using Polly;

namespace Bookify.App.Core.ViewModels
{
    public class SearchViewModel : BaseViewModel
    {
        private readonly IBooksService booksService;
        
        public SearchViewModel(IBooksService booksService)
        {
            this.booksService = booksService;

            this.Filtered = new ObservableServiceCollection<BookDto, BookFilter, IBooksService>(this.booksService);
        }

        public ObservableServiceCollection<BookDto, BookFilter, IBooksService> Filtered { get; }

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

            if (this.Genre != null)
            {
                this.Filtered.Filter.GenreIds = new[] { this.Genre.Id };
            }

            this.Filtered.Filter.SearchText = this.SearchText;
            await this.Filtered.Restart();
        }

        public async Task<DetailedBookDto> GetBook(int id)
        {
            var result = await
                         Policy.Handle<WebException>()
                             .RetryAsync()
                             .ExecuteAndCaptureAsync(async () => await this.booksService.GetBook(id));
            if (result.Outcome == OutcomeType.Failure)
            {
                return null;
            }
            return result.Result;
        }
    }
}