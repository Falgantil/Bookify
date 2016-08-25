using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

using Bookify.Common.Commands.Auth;
using Bookify.Common.Filter;
using Bookify.Common.Models;
using Bookify.Common.Repositories;
using Bookify.DataAccess.Models;

namespace Bookify.DataAccess.Repositories
{
    public class PublisherRepository : GenericRepository<Publisher>, IPublisherRepository
    {
        public PublisherRepository(BookifyContext context) : base(context)
        {

        }

        public async Task<PublisherDto> CreatePublisher(CreatePublisherCommand command)
        {
            var publisher = await this.Add(new Publisher { Name = command.Name });
            return publisher.ToDto();
        }

        public async Task<PublisherDto> EditPublisher(int id, EditPublisherCommand command)
        {
            var publisher = await this.Find(id);
            publisher.Name = command.Name;
            publisher = await this.Update(publisher);
            return publisher.ToDto();
        }

        public async Task<IPaginatedEnumerable<PublisherDto>> GetByFilter(PublisherFilter filter)
        {
            var searchText = (filter.Search ?? string.Empty).ToLower();

            var query = await this.GetAll();
            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(g => g.Name.ToLower().Contains(searchText));
            }

            query = query.OrderBy(g => g.Id);

            var totalCount = query.Count();
            query = query.Skip(filter.Skip);
            query = query.Take(filter.Take);
            var collection = await query.ToListAsync();
            return new PaginatedEnumerable<PublisherDto>(collection.Select(g => g.ToDto()), totalCount);
        }

        public async Task<PublisherDto> GetById(int id)
        {
            var publisher = await this.Find(id);
            return publisher.ToDto();
        }
    }
}
