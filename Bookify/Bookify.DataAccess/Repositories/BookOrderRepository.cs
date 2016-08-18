using Bookify.Common.Repositories;
using Bookify.DataAccess.Models;

namespace Bookify.DataAccess.Repositories
{
    public class BookOrderRepository : GenericRepository<BookOrder>, IBookOrderRepository
    {
        public BookOrderRepository(BookifyContext context) : base(context)
        {

        }
    }
}
