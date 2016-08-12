using Bookify.Core;
using Bookify.Models;

namespace Bookify.DataAccess.Repositories
{
    internal class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        internal AuthorRepository(BookifyContext ctx) : base(ctx)
        {

        }
    }
}
