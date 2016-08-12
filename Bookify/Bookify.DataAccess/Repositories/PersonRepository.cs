using Bookify.Core;
using Bookify.Models;

namespace Bookify.DataAccess.Repositories
{
    internal class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        internal PersonRepository(BookifyContext ctx) : base(ctx)
        {

        }
    }
}
