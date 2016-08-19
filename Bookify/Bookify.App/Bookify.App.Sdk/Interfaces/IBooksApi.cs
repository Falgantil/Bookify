using System.Collections.Generic;
using System.Threading.Tasks;
using Bookify.Common.Filter;
using Bookify.Common.Models;

namespace Bookify.App.Sdk.Interfaces
{
    public interface IBooksApi : IGetByFilterApi<BookDto, BookFilter>
    {
        Task<DetailedBookDto> Get(int id);
    }
}