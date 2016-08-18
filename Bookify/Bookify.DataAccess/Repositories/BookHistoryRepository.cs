using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bookify.Common.Commands.Auth;
using Bookify.Common.Models;
using Bookify.Common.Repositories;
using Bookify.DataAccess.Models;

namespace Bookify.DataAccess.Repositories
{
    public class BookHistoryRepository : GenericRepository<BookHistory>, IBookHistoryRepository
    {
        public BookHistoryRepository(BookifyContext context) : base(context)
        {

        }

        public async Task AddHistory(CreateHistoryCommand command)
        {
            var history = await this.Add(new BookHistory
            {
                // TODO: Implement me
            });
        }

        public async Task<IEnumerable<BookHistoryDto>> GetHistoryForBook(int bookId)
        {
            var histories = await this.Get(h => h.BookId == bookId);
            var bookHistories = await histories.ToListAsync(CancellationToken.None);
            return bookHistories.Select(h => h.ToDto());
        }

    }
}
