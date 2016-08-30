using Bookify.Common.Commands.Auth;
using Bookify.Common.Filter;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Bookify.API.Attributes;
using Bookify.Common.Models;
using Bookify.Common.Repositories;
using Swashbuckle.Swagger;

namespace Bookify.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Bookify.API.Controllers.BaseApiController" />
    [Auth]
    [RoutePrefix("publishers")]
    public class PublishersController : BaseApiController
    {
        private readonly IPublisherRepository _publisherRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishersController"/> class.
        /// </summary>
        /// <param name="publisherRepository">The publisher repository.</param>
        public PublishersController(IPublisherRepository publisherRepository)
        {
            this._publisherRepository = publisherRepository;
        }

        /// <summary>
        /// Gets the publisher specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(PublisherDto))]
        public async Task<IHttpActionResult> Get([FromUri]PublisherFilter filter)
        {
            return this.Ok(await this._publisherRepository.GetByFilter(filter));
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            return await this.Try(() => this._publisherRepository.GetById(id));
        }

        /// <summary>
        /// Creates the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody]CreatePublisherCommand command)
        {
            return await this.Try(async () => await this._publisherRepository.CreatePublisher(command));
        }

        /// <summary>
        /// Updates the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        [HttpPatch]
        [Route("{id}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody]EditPublisherCommand command)
        {
            return await this.Try(async () => await this._publisherRepository.EditPublisher(id, command));
        }
    }
}
