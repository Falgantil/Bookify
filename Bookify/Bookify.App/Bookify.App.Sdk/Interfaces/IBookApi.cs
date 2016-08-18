using System.Collections.Generic;
using System.Threading.Tasks;
using Bookify.Common.Models;

namespace Bookify.App.Sdk.Interfaces
{
    public interface IBookApi
    {
        Task<IEnumerable<BookDto>> GetAll(int index, int count, string searchText);

        Task<BookDto> Get(int id);
    }
}