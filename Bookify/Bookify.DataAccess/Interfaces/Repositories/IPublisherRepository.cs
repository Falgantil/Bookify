using System.Threading.Tasks;

using Bookify.Common.Commands.Auth;
using Bookify.Common.Filter;
using Bookify.Common.Models;
using Bookify.DataAccess.Models;
using Bookify.DataAccess.Models.ViewModels;

namespace Bookify.DataAccess.Interfaces.Repositories
{
    public interface IPublisherRepository : IGenericRepository<Publisher>
    {
        Task<PublisherDto> EditPublisher(UpdatePublisherCommand command);

        Task<PublisherDto> CreatePublisher(CreatePublisherCommand command);

        Task<IPaginatedEnumerable<PublisherDto>> GetByFilter(PublisherFilter filter);

        Task<PublisherDto> GetById(int id);
    }
}
