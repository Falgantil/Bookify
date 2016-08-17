using Bookify.Core.Interfaces.Repositories;
using Bookify.Models;

namespace Bookify.DataAccess.Repositories
{
    public class PersonRoleRepository : GenericRepository<PersonRole>, IPersonRoleRepository
    {
        public PersonRoleRepository(BookifyContext ctx) : base(ctx)
        {

        }
    }
}
