using System.Collections.Generic;
using System.Threading.Tasks;
using Bookify.Common.Filter;
using Bookify.Common.Models;

namespace Bookify.App.Sdk.Interfaces
{
    public interface IBooksApi
    {
        Task<DetailedBookDto> Get(int id);

        Task<IPaginatedEnumerable<BookDto>> GetBooks(BookFilter filter);
    }
}