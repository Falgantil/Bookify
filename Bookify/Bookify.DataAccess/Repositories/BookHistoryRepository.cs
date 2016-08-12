using Bookify.Core;
using Bookify.Models;

namespace Bookify.DataAccess.Repositories
{
    internal class BookHistoryRepository : GenericRepository<BookHistory>, IBookHistoryRepository
    {
        internal BookHistoryRepository(BookifyContext ctx) : base(ctx)
        {

        }
    }
}
