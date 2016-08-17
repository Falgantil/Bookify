using System.Collections.Generic;
using System.Threading.Tasks;
using Bookify.Models;

namespace Bookify.App.Sdk.Interfaces
{
    public interface IBookApi
    {
        Task<IEnumerable<Book>> GetAll(int index, int count, string searchText);

        Task<Book> Get(int id);
    }
}