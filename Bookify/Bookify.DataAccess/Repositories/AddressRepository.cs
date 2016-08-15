using Bookify.Models;
using Bookify.Core;
using Bookify.Core.Interfaces;

namespace Bookify.DataAccess.Repositories
{
    public class AddressRepository : GenericRepository<Address>, IAddressRepository
    {
        public AddressRepository(BookifyContext ctx) : base(ctx)
        {

        }
    }
}
