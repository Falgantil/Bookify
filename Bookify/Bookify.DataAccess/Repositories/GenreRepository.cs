using Bookify.Core;
using Bookify.Models;

namespace Bookify.DataAccess.Repositories
{
    internal class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        internal GenreRepository(BookifyContext ctx) : base(ctx)
        {

        }
    }
}
