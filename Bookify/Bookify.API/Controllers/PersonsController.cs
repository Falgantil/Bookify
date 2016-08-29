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
    [Auth]
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
            this._authenticationRepository = authenticationRepository;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            return await this.Try(async () => await this._personRepository.GetById(id));
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody]EditPersonCommand command)
        {
            return await this.Try(async () => await this._personRepository.EditPerson(id, command));
        }

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

        [HttpGet]
        [Route("me")]
        public async Task<IHttpActionResult> Me()
        {
            return await this.Try(async () =>
            {
                var person = await this.GetAuthorizedMember(this._authenticationRepository);
                return person.PersonDto;
            });
        }

        [HttpGet]
        [Route("subscribe")]
        public async Task<IHttpActionResult> HasSubscription()
        {
            return await this.Try(async () =>
            {
                var authDto = await this.GetAuthorizedMember(this._authenticationRepository);
                return await this._personRepository.HasSubscription(authDto.PersonDto.Id);
            });
        }

        [HttpPatch]
        [Route("me")]
        public async Task<IHttpActionResult> Me([FromBody]EditPersonCommand command)
        {
            return await this.Try(async () =>
            {
                var person = await this.GetAuthorizedMember(this._authenticationRepository);
                return this._personRepository.EditPerson(person.PersonDto.Id, command);
            });
        }
    }
}
