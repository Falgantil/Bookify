using System.Collections.Generic;
using System.Threading.Tasks;
using Bookify.Common.Models;

namespace Bookify.App.Core.Interfaces.Services
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetBooks(int index, int count, string searchText);

        Task<DetailedBookDto> GetBook(int id);
    }
}