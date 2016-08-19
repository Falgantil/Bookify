using System.Collections.Generic;
using System.Threading.Tasks;

using Bookify.App.Core.Interfaces.Services;
using Bookify.App.Sdk.Interfaces;
using Bookify.Common.Models;

namespace Bookify.App.Core.Services
{
    public class BookService : IBookService
    {
        private readonly IBookApi api;

        public BookService(IBookApi api)
        {
            this.api = api;
        }
        
        public async Task<IEnumerable<BookDto>> GetBooks(int index, int count, string searchText)
        {
            return await this.api.GetAll(index, count, searchText);
        }

        public async Task<BookDto> GetBook(int id)
        {
            return await this.api.Get(id);
        }
    }
}