using Bookify.Core;
using Bookify.Core.Interfaces;
using Bookify.Models;

namespace Bookify.DataAccess.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(BookifyContext ctx) : base(ctx)
        {

        }
    }
}