using System.Threading.Tasks;
using Bookify.App.Core.Collections;
using Bookify.App.Core.Interfaces.Services;
using Bookify.Common.Filter;
using Bookify.Common.Models;

namespace Bookify.App.Core.ViewModels
{
    public class FeaturedViewModel : BaseViewModel
    {
        /// <summary>
        /// The books service
        /// </summary>
        private readonly IBooksService booksService;

        /// <summary>
        /// Initializes a new instance of the <see cref="FeaturedViewModel"/> class.
        /// </summary>
        /// <param name="booksService">The books service.</param>
        public FeaturedViewModel(IBooksService booksService)
        {
            this.booksService = booksService;
            this.Books = new ObservableServiceCollection<BookDto, BookFilter, IBooksService>(this.booksService);
        }

        /// <summary>
        /// Gets the books.
        /// </summary>
        /// <value>
        /// The books.
        /// </value>
        public ObservableServiceCollection<BookDto, BookFilter, IBooksService> Books { get; }

        /// <summary>
        /// Gets the book with the specified <see cref="id"/>.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<DetailedBookDto> GetBook(int id)
        {
            return await this.booksService.GetBook(id);
        }
    }
}