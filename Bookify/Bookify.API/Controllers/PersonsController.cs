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
            var token = this.Request.Headers.Authorization.Parameter;
            try
            {
                return Ok(_authenticationRepository.VerifyToken(token));
            }
            catch (ApiException ex)
            {
                return this.Content((HttpStatusCode)ex.StatusCode, new
                {
                    Message = ex.Message,
                    StatusCode = ex.StatusCode
                });
            }
            catch (Exception e)
            {
                return this.Content(
                    HttpStatusCode.InternalServerError,
                    new
                    {
                        Message = "An unknown error occurred on the server.",
                        ExceptionMessage = e.Message,
                        ExceptionInnerMessage = e.InnerException?.Message
                    });
            }
        }

        [HttpPost]
        [Auth]
        [Route("{id}/subscribe")]
        public async Task<IHttpActionResult> Subscribe(int id, decimal paid)
        {
            return await this.Try(async () => await _personRepository.Subscibe(id, paid));
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
