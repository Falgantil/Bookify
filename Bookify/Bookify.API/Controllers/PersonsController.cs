using Bookify.Common.Commands.Auth;
using System.Threading.Tasks;
using System.Web.Http;
using Bookify.Common.Repositories;

namespace Bookify.API.Controllers
{
    [RoutePrefix("persons")]
    public class PersonsController : BaseApiController
    {
        private readonly IPersonRepository _personRepository;
        private readonly IAuthenticationRepository _authenticationRepository;

        public PersonsController(
            IPersonRepository personRepository, 
            IAuthenticationRepository authenticationRepository)
        {
            this._personRepository = personRepository;
            _authenticationRepository = authenticationRepository;
        }

        [HttpPost]
        [Authorize]
        [Route("{id}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody]UpdatePersonCommand command)
        {
            return await this.Try(async () => await this._personRepository.EditPerson(id, command));
        }

        [HttpGet]
        [Authorize]
        [Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            return await this.Try(async () => await this._personRepository.GetById(id));
        }

        [HttpGet]
        [Authorize]
        [Route("me")]
        public async Task<IHttpActionResult> Me()
        {
            var token = this.Request.Headers.Authorization.Parameter;
            return await this.Try(async () => await _authenticationRepository.VerifyToken(token));
        }

        [HttpPost]
        [Authorize]
        [Route("{id}/subscribe")]
        public async Task<IHttpActionResult> Subscribe(int id, decimal paid)
        {
            return await this.Try(async () => await _personRepository.Subscibe(id, paid));
        }

        [HttpPost]
        [Authorize]
        [Route("{id}/subscribe")]
        public async Task<IHttpActionResult> HasSubscribe(int id)
        {
            return await this.Try(async () => await _personRepository.HasSubscription(id));
        }
    }
}
