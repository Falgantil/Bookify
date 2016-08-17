using System.Collections.Generic;
using System.Threading.Tasks;

using Bookify.App.Core.Models;
using Bookify.Models;

namespace Bookify.App.Core.Interfaces.Services
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetBooks(int index, int count, string searchText);

        Task<Book> GetBook(int id);
    }
}