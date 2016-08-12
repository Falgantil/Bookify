using Bookify.Core;
using Bookify.Models;

namespace Bookify.DataAccess.Repositories
{
    internal class BookOrderRepository : GenericRepository<BookOrder>, IBookOrderRepository
    {
        internal BookOrderRepository(BookifyContext ctx) : base(ctx)
        {

        }
    }
}
