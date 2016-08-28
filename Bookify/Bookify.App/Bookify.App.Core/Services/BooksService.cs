using System.Threading.Tasks;
using Bookify.App.Core.Collections;
using Bookify.App.Core.Interfaces.Services;
using Bookify.App.Sdk.Interfaces;
using Bookify.Common.Filter;
using Bookify.Common.Models;

namespace Bookify.App.Core.Services
{
    /// <summary>
    /// The Book Service implementation
    /// </summary>
    /// <seealso cref="IBooksService" />
    public class BooksService : IBooksService
    {
        /// <summary>
        /// The Books API
        /// </summary>
        private readonly IBooksApi booksApi;

        /// <summary>
        /// The Files API
        /// </summary>
        private readonly IFilesApi filesApi;

        /// <summary>
        /// Initializes a new instance of the <see cref="BooksService" /> class.
        /// </summary>
        /// <param name="booksApi">The books API.</param>
        /// <param name="filesApi">The files API.</param>
        public BooksService(IBooksApi booksApi, IFilesApi filesApi)
        {
            this.booksApi = booksApi;
            this.filesApi = filesApi;
            this.MyBooks = new ObservableServiceCollection<BookDto, BookFilter, IBooksService>(
                this,
                new BookFilter { MyBooks = true });
        }

        /// <summary>
        /// Gets my books.
        /// </summary>
        /// <value>
        /// My books.
        /// </value>
        public ObservableServiceCollection<BookDto, BookFilter, IBooksService> MyBooks { get; }

        /// <summary>
        /// Gets the book with the specified ID.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<DetailedBookDto> GetBook(int id)
        {
            return await this.booksApi.Get(id);
        }

        /// <summary>
        /// Gets the items, using the provided Filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public async Task<IPaginatedEnumerable<BookDto>> GetItems(BookFilter filter)
        {
            if (!filter.MyBooks)
            {
                return await this.booksApi.GetBooks(filter);
            }
            return await this.booksApi.GetMyBooks(filter);
        }

        /// <summary>
        /// Downloads the book as an Epub file.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<byte[]> DownloadBook(int id)
        {
            return await this.filesApi.DownloadBook(id);
        }
    }
}