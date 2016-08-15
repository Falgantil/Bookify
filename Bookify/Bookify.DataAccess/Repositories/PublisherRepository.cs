using Bookify.Core;
using Bookify.Core.Interfaces;
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
