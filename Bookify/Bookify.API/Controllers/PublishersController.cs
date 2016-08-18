using Bookify.Common.Commands.Auth;
using Bookify.Common.Filter;
using Bookify.DataAccess.Interfaces.Repositories;
using System.Threading.Tasks;
using System.Web.Http;

namespace Bookify.API.Controllers
{
    [Authorize]
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
            return Ok(await this.publisherRepository.GetByFilter(filter));
        }

        [HttpPut]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody]CreatePublisherCommand command)
        {
            return await this.Try(async () => await this.publisherRepository.CreatePublisher(command));
        }

        [HttpPost]
        [Route("{id}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody]UpdatePublisherCommand command)
        {
            command.Id = id;
            return await this.Try(async () => await this.publisherRepository.EditPublisher(command));
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            return await this.Try(() => this.publisherRepository.GetById(id));
        }
    }
}
