using Bookify.Core;
using Bookify.Models;

namespace Bookify.DataAccess.Repositories
{
    internal class PersonRoleRepository : GenericRepository<PersonRole>, IPersonRoleRepository
    {
        internal PersonRoleRepository(BookifyContext ctx) : base(ctx)
        {

        }
    }
}
