using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Bookify.Common.Models;
using Bookify.DataAccess.Interfaces.Repositories;
using Bookify.DataAccess.Models;
using Bookify.DataAccess.Models.ViewModels;

namespace Bookify.DataAccess.Repositories
{
    public class BookHistoryRepository : GenericRepository<BookHistory>, IBookHistoryRepository
    {
        public BookHistoryRepository(BookifyContext context) : base(context)
        {

        }

        public async Task AddHistory(BookHistory bookHistory)
        {
            await this.Add(bookHistory);
        }

        public async Task<IEnumerable<BookHistoryDto>> GetHistoryForBook(int bookId)
        {
            var histories = await this.Get(h => h.BookId == bookId);
            var bookHistories = await histories.ToListAsync(CancellationToken.None);
            return bookHistories.Select(h => h.ToDto());
        }

    }
}
