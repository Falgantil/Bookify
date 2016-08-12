using Bookify.Core;
using Bookify.Models;

namespace Bookify.DataAccess.Repositories
{
    public class PublisherRepository : GenericRepository<Publisher>, IPublisherRepository
    {
        public PublisherRepository(BookifyContext ctx) : base(ctx)
        {

        }
    }
}
