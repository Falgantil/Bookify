using Bookify.Common.Commands.Auth;
using Bookify.Common.Filter;
using System.Threading.Tasks;
using System.Web.Http;
using Bookify.API.Attributes;
using Bookify.Common.Repositories;

namespace Bookify.API.Controllers
{
    [Auth]
    [RoutePrefix("publishers")]
    public class PublishersController : BaseApiController
    {
        private readonly IPublisherRepository publisherRepository;

        public PublishersController(IPublisherRepository publisherRepository)
        {
            this.publisherRepository = publisherRepository;
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get([FromUri]PublisherFilter filter)
        {
            return this.Ok(await this.publisherRepository.GetByFilter(filter));
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            return await this.Try(() => this.publisherRepository.GetById(id));
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody]CreatePublisherCommand command)
        {
            return await this.Try(async () => await this.publisherRepository.CreatePublisher(command));
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody]EditPublisherCommand command)
        {
            return await this.Try(async () => await this.publisherRepository.EditPublisher(id, command));
        }
    }
}
