using System.Threading.Tasks;
using Bookify.Common.Commands.Auth;
using Bookify.Common.Filter;
using Bookify.Common.Models;

namespace Bookify.Common.Repositories
{
    public interface IPublisherRepository
    {
        Task<PublisherDto> EditPublisher(UpdatePublisherCommand command);

        Task<PublisherDto> CreatePublisher(CreatePublisherCommand command);

        Task<IPaginatedEnumerable<PublisherDto>> GetByFilter(PublisherFilter filter);

        Task<PublisherDto> GetById(int id);
    }
}
