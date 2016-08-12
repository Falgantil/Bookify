using Bookify.Models;
using Bookify.Core;

namespace Bookify.DataAccess.Repositories
{
    public class AddressRepository : GenericRepository<Address>, IAddressRepository
    {
        public AddressRepository(BookifyContext ctx) : base(ctx)
        {

        }
    }
}
