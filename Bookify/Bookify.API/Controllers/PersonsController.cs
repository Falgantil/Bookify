using Bookify.Common.Commands.Auth;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Bookify.API.Attributes;
using Bookify.Common.Models;
using Bookify.Common.Repositories;

namespace Bookify.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Bookify.API.Controllers.BaseApiController" />
    [Auth]
    [RoutePrefix("persons")]
    public class PersonsController : BaseApiController
    {
        private readonly IPersonRepository _personRepository;
        private readonly IAuthenticationRepository _authenticationRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonsController"/> class.
        /// </summary>
        /// <param name="personRepository">The person repository.</param>
        /// <param name="authenticationRepository">The authentication repository.</param>
        public PersonsController(
            IPersonRepository personRepository, 
            IAuthenticationRepository authenticationRepository)
        {
            this._personRepository = personRepository;
            this._authenticationRepository = authenticationRepository;
        }

        /// <summary>
        /// Gets the person specified by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <response code="200" cref="Get(int)">OK</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ResponseType(typeof(PersonDto))]
        public async Task<IHttpActionResult> Get(int id)
        {
            return await this.Try(async () => await this._personRepository.GetById(id));
        }

        /// <summary>
        /// Updates the person specified by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="command">The command.</param>
        /// <response code="200" cref="Update">OK</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns></returns>
        [HttpPatch]
        [Route("{id}")]
        [ResponseType(typeof(PersonDto))]
        public async Task<IHttpActionResult> Update(int id, [FromBody]EditPersonCommand command)
        {
            return await this.Try(async () => await this._personRepository.EditPerson(id, command));
        }

        /// <summary>
        /// Subscribes monthly.
        /// </summary>
        /// <response code="200" cref="Subscribe">OK</response>
        /// <response code="400">Bad Request Error</response>
        /// <response code="404">Not found Error</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns></returns>
        [HttpPost]
        [Route("subscribe")]
        public async Task<IHttpActionResult> Subscribe()
        {
            return await this.Try(async () =>
            {
                var personAuthDto = await this.GetAuthorizedMember(this._authenticationRepository);
                await this._personRepository.Subscribe(personAuthDto.PersonDto.Id);
            });
        }

        /// <summary>
        /// Gets the current logged in person.
        /// </summary>
        /// <response code="200" cref="Me()">OK</response>
        /// <response code="400">Bad Request Error</response>
        /// <response code="404">Not found Error</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns></returns>
        [HttpGet]
        [Route("me")]
        [ResponseType(typeof(PersonDto))]
        public async Task<IHttpActionResult> Me()
        {
            return await this.Try(async () =>
            {
                var personAuthDto = await this.GetAuthorizedMember(this._authenticationRepository);
                return personAuthDto.PersonDto;
            });
        }

        /// <summary>
        /// Determines whether this person has a subscription.
        /// </summary>
        /// <response code="200" cref="HasSubscription">OK</response>
        /// <response code="400">Bad Request Error</response>
        /// <response code="404">Not found Error</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns></returns>
        [HttpGet]
        [Route("subscribe")]
        public async Task<IHttpActionResult> HasSubscription()
        {
            return await this.Try(async () =>
            {
                var personAuthDto = await this.GetAuthorizedMember(this._authenticationRepository);
                return await this._personRepository.HasSubscription(personAuthDto.PersonDto.Id);
            });
        }

        /// <summary>
        /// Updates the current user specified by auth token.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <response code="200" cref="Me(Bookify.Common.Commands.Auth.EditPersonCommand)">OK</response>
        /// <response code="400">Bad Request Error</response>
        /// <response code="404">Not found Error</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns></returns>
        [HttpPatch]
        [Route("me")]
        public async Task<IHttpActionResult> Me([FromBody]EditPersonCommand command)
        {
            return await this.Try(async () =>
            {
                var personAuthDto = await this.GetAuthorizedMember(this._authenticationRepository);
                return this._personRepository.EditPerson(personAuthDto.PersonDto.Id, command);
            });
        }
    }
}
