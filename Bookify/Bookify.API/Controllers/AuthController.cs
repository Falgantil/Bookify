using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Bookify.Core;
using Bookify.Core.Exceptions;
using Bookify.Core.Interfaces;
using Bookify.Core.Interfaces.Services;
using Newtonsoft.Json;

namespace Bookify.API.Controllers
{
    public class AuthController : ApiController
    {
        private readonly IAuthenticationService _authService;

        public AuthController(IAuthenticationService authService)
        {
            this._authService = authService;
        }

        [HttpPost]
        public async Task<IHttpActionResult> Login(string email, string password)
        {
            try
            {
                var result = await this._authService.Login(email, password);
                return Ok(new
                {
                    accessToken = result
                });
            }
            catch (AuthenticationException ex)
            {
                return Content(HttpStatusCode.Unauthorized, new
                {
                    message = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> Register(string username, string firstname, string lastname, string email, string password)
        {
            try
            {
                var newPerson = await this._authService.Register(username, firstname, lastname, email, password);
                return await Login(email, password);
            }
            catch (PersonRegistrationException ex)
            {
                return Content(HttpStatusCode.BadRequest, new
                {
                    message = $"An account already exists with email {ex.Email}"
                });
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IHttpActionResult> Logout()
        {
            return Ok();
        }
    }
}
