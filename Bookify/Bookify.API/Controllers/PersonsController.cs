﻿using System.Threading.Tasks;
using System.Web.Http;

using Bookify.Common.Commands.Auth;
using Bookify.DataAccess.Interfaces.Repositories;
using Bookify.DataAccess.Models;
using Bookify.Models;

namespace Bookify.API.Controllers
{
    [RoutePrefix("peopl")]
    public class PersonsController : BaseApiController
    {
        private readonly IPersonRepository personRepository;

        public PersonsController(IPersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }
        
        [HttpPost]
        [Authorize]
        [Route("")]
        public async Task<IHttpActionResult> Update([FromBody]UpdatePersonCommand command)
        {
            return await this.Try(async () => await this.personRepository.EditPerson(command));
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
