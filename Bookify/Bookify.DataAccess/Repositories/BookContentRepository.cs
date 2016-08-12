using Bookify.Core;
using Bookify.Models;

namespace Bookify.DataAccess.Repositories
{
    internal class BookContentRepository : GenericRepository<BookContent>, IBookContentRepository
    {
        internal BookContentRepository(BookifyContext ctx) : base(ctx)
        {

        }
    }
}
