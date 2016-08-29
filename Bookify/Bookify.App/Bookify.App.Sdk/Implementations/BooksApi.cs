using System.Threading.Tasks;
using Bookify.App.Sdk.Interfaces;
using Bookify.Common.Filter;
using Bookify.Common.Models;
using Newtonsoft.Json;

namespace Bookify.App.Sdk.Implementations
{
    /// <summary>
    /// The Books Api Implementation.
    /// </summary>
    /// <seealso cref="BaseApi" />
    /// <seealso cref="IBooksApi" />
    public class BooksApi : BaseApi, IBooksApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BooksApi"/> class.
        /// </summary>
        public BooksApi()
            : base(ApiConfig.BooksRoot)
        {
        }

        /// <summary>
        /// Gets a collection of published books, filtered by <see cref="filter" />.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public async Task<IPaginatedEnumerable<BookDto>> GetBooks(BookFilter filter)
        {
            var request = new RequestBuilder()
                .BaseUri(this.Url)
                .AddQuery(nameof(filter.Skip), filter.Skip)
                .AddQuery(nameof(filter.Take), filter.Take);

            if (!string.IsNullOrEmpty(filter.Search))
            {
                request.AddQuery(nameof(filter.Search), filter.Search);
            }

            if (filter.Genres != null)
            {
                foreach (var genreId in filter.Genres)
                {
                    request.AddQuery(nameof(filter.Genres), genreId);
                }
            }
            
            return await this.ExecuteAndParse<PaginatedEnumerable<BookDto>>(request);
        }

        /// <summary>
        /// Gets my books, filtered by <see cref="filter" />.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public async Task<IPaginatedEnumerable<BookDto>> GetMyBooks(BookFilter filter)
        {
            var request = new RequestBuilder()
                .BaseUri(this.CombineUrl("mybooks"))
                .AddQuery(nameof(filter.Skip), filter.Skip)
                .AddQuery(nameof(filter.Take), filter.Take);

            if (!string.IsNullOrEmpty(filter.Search))
            {
                request.AddQuery(nameof(filter.Search), filter.Search);
            }

            if (filter.Genres != null)
            {
                foreach (var genreId in filter.Genres)
                {
                    request.AddQuery(nameof(filter.Genres), genreId);
                }
            }

            return await this.ExecuteAndParse<PaginatedEnumerable<BookDto>>(request);
        }

        /// <summary>
        /// Gets the book by the specified identifier.
        /// </summary>
        /// <param name="id">The book identifier.</param>
        /// <returns></returns>
        public async Task<DetailedBookDto> Get(int id)
        {
            var request = new RequestBuilder()
                .BaseUri(this.CombineUrl("{id}"))
                .AddUriSegment("id", id);

            var response = await this.ExecuteRequest(request);
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<DetailedBookDto>(json);
        }
    }
}