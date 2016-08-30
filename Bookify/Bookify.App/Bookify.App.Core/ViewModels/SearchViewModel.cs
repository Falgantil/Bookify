using System.Threading.Tasks;

using Bookify.App.Core.Collections;
using Bookify.App.Core.Interfaces.Services;
using Bookify.Common.Filter;
using Bookify.Common.Models;

namespace Bookify.App.Core.ViewModels
{
    public class SearchViewModel : BaseViewModel
    {
        /// <summary>
        /// The books service
        /// </summary>
        private readonly IBooksService booksService;

        /// <summary>
        /// The delay service
        /// </summary>
        private readonly IDelayService delayService;

        /// <summary>
        /// The delays
        /// </summary>
        private int delays;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchViewModel"/> class.
        /// </summary>
        /// <param name="booksService">The books service.</param>
        /// <param name="delayService">The delay service.</param>
        public SearchViewModel(IBooksService booksService, IDelayService delayService)
        {
            this.booksService = booksService;
            this.delayService = delayService;

            this.Filtered = new ObservableServiceCollection<BookDto, BookFilter, IBooksService>(this.booksService);
        }

        /// <summary>
        /// Gets the filtered.
        /// </summary>
        /// <value>
        /// The filtered.
        /// </value>
        public ObservableServiceCollection<BookDto, BookFilter, IBooksService> Filtered { get; }

        /// <summary>
        /// Gets or sets the search text.
        /// </summary>
        /// <value>
        /// The search text.
        /// </value>
        public string SearchText { get; set; }

        /// <summary>
        /// Gets or sets the genre.
        /// <para>Can be null. If null, it will simply not search in a specific genre.</para>
        /// </summary>
        /// <value>
        /// The genre.
        /// </value>
        public GenreDto Genre { get; set; }

        /// <summary>
        /// Refreshes the content.
        /// </summary>
        public void RefreshContent()
        {
            this.RefreshSearchTimer();
        }

        /// <summary>
        /// Gets the book.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<DetailedBookDto> GetBook(int id)
        {
            return await this.booksService.GetBook(id);
        }

        /// <summary>
        /// Called when the <see cref="SearchText"/> property has changed.
        /// <para>Fody handles invocation of this method.</para>
        /// </summary>
        private void OnSearchTextChanged()
        {
            this.RefreshSearchTimer();
        }

        /// <summary>
        /// Refreshes the search timer.
        /// <para>When it expires, invokes <see cref="Filtered"/>'s Restart method, with the genre and search text as parameter.</para>
        /// </summary>
        private async void RefreshSearchTimer()
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
    }
}