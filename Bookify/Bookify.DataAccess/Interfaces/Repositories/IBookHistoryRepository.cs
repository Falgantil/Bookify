using System.Collections.Generic;
using System.Threading.Tasks;

using Bookify.Common.Models;
using Bookify.DataAccess.Models;
using Bookify.DataAccess.Models.ViewModels;

namespace Bookify.DataAccess.Interfaces.Repositories
{
    public interface IBookHistoryRepository : IGenericRepository<BookHistory>
    {
        Task AddHistory(BookHistory bookHistory);

        Task<IEnumerable<BookHistoryDto>> GetHistoryForBook(int bookId);
    }
}
