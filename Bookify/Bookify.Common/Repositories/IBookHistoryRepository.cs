using System.Collections.Generic;
using System.Threading.Tasks;
using Bookify.Common.Commands.Auth;
using Bookify.Common.Models;

namespace Bookify.Common.Repositories
{
    public interface IBookHistoryRepository
    {
        Task AddHistory(CreateHistoryCommand command);

        Task<IEnumerable<BookHistoryDto>> GetHistoryForBook(int bookId);
    }
}
