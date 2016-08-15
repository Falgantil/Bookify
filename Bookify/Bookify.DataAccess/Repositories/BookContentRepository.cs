using Bookify.Core;
using Bookify.Core.Interfaces;
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
