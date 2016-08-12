using Bookify.Core;
using Bookify.Models;

namespace Bookify.DataAccess.Repositories
{
    public class BookHistoryRepository : GenericRepository<BookHistory>, IBookHistoryRepository
    {
        public BookHistoryRepository(BookifyContext ctx) : base(ctx)
        {

        }
    }
}
