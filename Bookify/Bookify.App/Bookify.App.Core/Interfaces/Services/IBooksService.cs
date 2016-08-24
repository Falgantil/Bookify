using System.Collections.Generic;
using System.Threading.Tasks;

using Bookify.Common.Filter;
using Bookify.Common.Models;

namespace Bookify.App.Core.Interfaces.Services
{
    /// <summary>
    /// The Book Service interface. 
    /// Inherits from <see cref="IGetByFilterService{TDto,TFilter}"/>, 
    /// and contains extra methods relevant for the Book service.
    /// </summary>
    /// <seealso cref="Services.IGetByFilterService{BookDto, BookFilter}" />
    public interface IBooksService : IGetByFilterService<BookDto, BookFilter>
    {
        /// <summary>
        /// Gets the book with the specified ID.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<DetailedBookDto> GetBook(int id);
    }
}