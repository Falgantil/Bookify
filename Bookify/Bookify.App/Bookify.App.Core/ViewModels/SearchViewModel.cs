using System.Threading.Tasks;

using Bookify.App.Core.Collections;
using Bookify.App.Core.Interfaces.Services;
using Bookify.Common.Filter;
using Bookify.Common.Models;

namespace Bookify.App.Core.ViewModels
{
    public class SearchViewModel : BaseViewModel
    {
        private readonly IBooksService booksService;
        private readonly IDelayService delayService;

        public SearchViewModel(IBooksService booksService, IDelayService delayService)
        {
            this.booksService = booksService;
            this.delayService = delayService;

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

            await this.delayService.Delay(1000);

            this.delays--;

            if (this.delays > 0)
            {
                return;
            }

            this.Filtered.Filter.Genres = this.Genre != null ? new[] { this.Genre.Id } : null;
            this.Filtered.Filter.Search = !string.IsNullOrEmpty(this.SearchText) ? this.SearchText : null;

            await this.Filtered.Restart();
        }

        public async Task<DetailedBookDto> GetBook(int id)
        {
            return await this.booksService.GetBook(id);
        }
    }
}