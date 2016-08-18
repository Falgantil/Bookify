using Bookify.Common.Commands.Auth;
using Bookify.DataAccess.Interfaces.Repositories;
using System.Threading.Tasks;
using System.Web.Http;

namespace Bookify.API.Controllers
{
    [RoutePrefix("person")]
    public class PersonsController : BaseApiController
    {
        private readonly IPersonRepository personRepository;

        public PersonsController(IPersonRepository personRepository)
        {
            this.personRepository = personRepository;
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

        [HttpPost]
        [Authorize]
        public async Task<IHttpActionResult> Subscribe(int id)
        {
            //return Ok(await personRepository.Subscribe(id));
            return Ok();
        }
    }
}
