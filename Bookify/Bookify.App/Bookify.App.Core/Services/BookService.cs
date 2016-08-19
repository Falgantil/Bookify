using System.Collections.Generic;
using System.Threading.Tasks;

using Bookify.App.Core.Interfaces.Services;
using Bookify.App.Sdk.Interfaces;
using Bookify.Common.Filter;
using Bookify.Common.Models;

namespace Bookify.App.Core.Services
{
    public class BookService : IBookService
    {
        private readonly IBooksApi api;

        public BookService(IBooksApi api)
        {
            this.api = api;
        }
        
        public async Task<IEnumerable<BookDto>> GetBooks(int index, int count, string searchText)
        {
            return await this.api.GetItems(new BookFilter
            {
                Index = index,
                Count = count,
                SearchText = searchText
            });
        }

        public async Task<DetailedBookDto> GetBook(int id)
        {
            return await this.api.Get(id);
        }
    }
}