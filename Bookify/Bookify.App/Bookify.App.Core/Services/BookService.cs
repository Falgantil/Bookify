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

        //public async Task<IEnumerable<Book>> GetBooks(int index, int count, string searchText)
        //{
        //    await Task.Delay(500);

        //    var books = new List<Book>
        //    {
        //        new Book
        //        {
        //            Id = 1,
        //            Title = "Harry Potter and the Order of the Phoenix",
        //        },
        //        new Book
        //        {
        //            Id = 2,
        //            Title = "Game of Thrones - A song of Ice and Fire",
        //        },
        //        new Book
        //        {
        //            Id = 3,
        //            Title = "Fifty Shades of Grey",
        //        },
        //    };
        //    var array = new List<Book>();
        //    for (int i = index; i < index + count && books.Count > i; i++)
        //    {
        //        array.Add(books[i]);
        //    }
        //    return array;
        //}

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