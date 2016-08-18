using Bookify.Common.Repositories;
using Bookify.DataAccess.Models;

namespace Bookify.DataAccess.Repositories
{
    public class PersonRoleRepository : GenericRepository<PersonRole>, IPersonRoleRepository
    {
        public PersonRoleRepository(BookifyContext context) : base(context)
        {

        }
    }
}
