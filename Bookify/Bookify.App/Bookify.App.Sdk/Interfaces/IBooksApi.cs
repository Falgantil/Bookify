using System.Threading.Tasks;
using Bookify.Common.Filter;
using Bookify.Common.Models;

namespace Bookify.App.Sdk.Interfaces
{
    /// <summary>
    /// The Books Api Interface
    /// </summary>
    public interface IBooksApi
    {
        /// <summary>
        /// Gets a collection of published books, filtered by <see cref="filter"/>.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        Task<IPaginatedEnumerable<BookDto>> GetBooks(BookFilter filter);

        /// <summary>
        /// Gets my books, filtered by <see cref="filter"/>.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        Task<IPaginatedEnumerable<BookDto>> GetMyBooks(BookFilter filter);

        /// <summary>
        /// Gets the book by the specified identifier.
        /// </summary>
        /// <param name="id">The book identifier.</param>
        /// <returns></returns>
        Task<DetailedBookDto> Get(int id);
    }
}