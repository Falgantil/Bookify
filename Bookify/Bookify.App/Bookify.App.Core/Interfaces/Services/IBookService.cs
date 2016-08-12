using System.Collections.Generic;
using System.Threading.Tasks;

using Bookify.App.Core.Models;

namespace Bookify.App.Core.Interfaces.Services
{
    public interface IBookService
    {
        Task<IEnumerable<LightBookModel>> GetBooks(int index, int count);
        Task<BookModel> GetBook(int id);
    }
}