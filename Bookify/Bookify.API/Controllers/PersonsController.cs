using System;
using System.Net;
using Bookify.Common.Commands.Auth;
using System.Threading.Tasks;
using System.Web.Http;
using Bookify.API.Attributes;
using Bookify.Common.Exceptions;
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
        [Auth]
        [Route("{id}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody]UpdatePersonCommand command)
        {
            return await this.Try(async () => await this._personRepository.EditPerson(id, command));
        }

        [HttpGet]
        [Auth]
        [Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            return await this.Try(async () => await this._personRepository.GetById(id));
        }

        [HttpGet]
        [Auth]
        [Route("me")]
        public async Task<IHttpActionResult> Me()
        {
            return await this.Try(async () =>
            {
                var person = await this.GetAuthorizedMember(_authenticationRepository);
                return person.PersonDto;
            });
        }

        [HttpPost]
        [Auth]
        [Route("subscribe")]
        public async Task<IHttpActionResult> Subscribe(decimal paid)
        {
            return await this.Try(async () =>
            {
                var personAuthDto = await this.GetAuthorizedMember(this._authenticationRepository);
                await this._personRepository.Subscribe(personAuthDto.PersonDto.Id, paid);
            });
        }

        [HttpPost]
        [Auth]
        [Route("{id}/subscribe")]
        public async Task<IHttpActionResult> HasSubscribe(int id)
        {
            return await this.Try(async () => await _personRepository.HasSubscription(id));
        }
    }
}
