using Bookify.Core;
using Bookify.Models;

namespace Bookify.DataAccess.Repositories
{
    public class BookContentRepository : GenericRepository<BookContent>, IBookContentRepository
    {
        public BookContentRepository(BookifyContext ctx) : base(ctx)
        {

        }
    }
}
