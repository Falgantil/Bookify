using Bookify.Core;
using Bookify.Core.Interfaces;
using Bookify.Models;

namespace Bookify.DataAccess.Repositories
{
    public class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        public GenreRepository(BookifyContext ctx) : base(ctx)
        {

        }
    }
}
