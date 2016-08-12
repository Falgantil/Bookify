using Bookify.Core;
using Bookify.Models;

namespace Bookify.DataAccess
{
    internal class BookRepository : GenericRepository<Book>, IBookRepository
    {
        internal BookRepository(BookifyContext ctx) : base(ctx)
        {

        }
    }
}