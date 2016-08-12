using Bookify.Models;
using Bookify.Core;

namespace Bookify.DataAccess.Repositories
{
    internal class AddressRepository : GenericRepository<Address>, IAddressRepository
    {
        internal AddressRepository(BookifyContext ctx) : base(ctx)
        {

        }
    }
}
