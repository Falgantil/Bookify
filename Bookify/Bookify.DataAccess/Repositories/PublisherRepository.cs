using Bookify.Core;
using Bookify.Models;

namespace Bookify.DataAccess.Repositories
{
    internal class PublisherRepository : GenericRepository<Publisher>, IPublisherRepository
    {
        internal PublisherRepository(BookifyContext ctx) : base(ctx)
        {

        }
    }
}
