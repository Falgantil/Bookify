using Bookify.Common.Commands.Auth;
using System.Threading.Tasks;
using System.Web.Http;
using Bookify.Common.Repositories;

namespace Bookify.API.Controllers
{
    [RoutePrefix("person")]
    public class PersonsController : BaseApiController
    {
        private readonly IPersonRepository personRepository;
        private readonly IAuthenticationRepository _authenticationRepository;

        public PersonsController(IPersonRepository personRepository, IAuthenticationRepository authenticationRepository)
        {
            this.personRepository = personRepository;
            _authenticationRepository = authenticationRepository;
        }

        [HttpPost]
        [Authorize]
        [Route("{id}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody]UpdatePersonCommand command)
        {
            return await this.Try(async () => await this.personRepository.EditPerson(id, command));
        }

        [HttpGet]
        [Authorize]
        [Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            return await this.Try(async () => await this.personRepository.GetById(id));
        }

        [HttpGet]
        //[Authorize]
        [Route("me")]
        public async Task<IHttpActionResult> Me()
        {
            var t =this.Request.Headers.Authorization.Parameter;
            return await this.Try(async () => await _authenticationRepository.VerifyToken(t));
        }

        [HttpPost]
        [Authorize]
        public async Task<IHttpActionResult> Subscribe(int id)
        {
            //return Ok(await personRepository.Subscribe(id));
            return Ok();
        }
    }
}
