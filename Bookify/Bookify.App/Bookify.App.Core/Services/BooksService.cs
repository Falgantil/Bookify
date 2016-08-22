using System.Threading.Tasks;

using Bookify.App.Core.Interfaces.Services;
using Bookify.App.Sdk.Interfaces;
using Bookify.Common.Filter;
using Bookify.Common.Models;

namespace Bookify.App.Core.Services
{
    public class BooksService : IBooksService
    {
        private readonly IBooksApi api;

        public BooksService(IBooksApi api)
        {
            this.api = api;
        }
        
        public async Task<DetailedBookDto> GetBook(int id)
        {
            return await this.api.Get(id);
        }

        public async Task<IPaginatedEnumerable<BookDto>> GetItems(BookFilter filter)
        {
            return await this.api.GetBooks(filter);
        }
    }
}