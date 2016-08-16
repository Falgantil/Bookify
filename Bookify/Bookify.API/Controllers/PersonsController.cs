using System;
using System.Threading.Tasks;
using System.Web.Http;
using Bookify.Core.Interfaces;
using Bookify.Models;

namespace Bookify.API.Controllers
{
    public class PersonsController : ApiController
    {
        private IPersonRepository _personRepository;

        public PersonsController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }


        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            return Ok();
        }

        [HttpPut]
        [Authorize]
        public async Task<IHttpActionResult> Create(Person person)
        {
            return Ok(await _personRepository.Add(person));
        }

        [HttpPost]
        [Authorize]
        public async Task<IHttpActionResult> Update(Person person)
        {
            return Ok(await _personRepository.Update(person));
        }

        [HttpGet]
        [Authorize]
        public async Task<IHttpActionResult> Get(int id)
        {
            return Ok(await _personRepository.Find(id));
        }

        [HttpPost]
        [Authorize]
        public async Task<IHttpActionResult> Subscribe(int id)
        {
            //return Ok(await _personRepository.Subscribe(id));
            return Ok();
        }
    }
}
