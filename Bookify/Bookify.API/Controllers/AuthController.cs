using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Description;
using Bookify.Common.Commands.Auth;
using Bookify.Common.Exceptions;
using Bookify.Common.Filter;
using Bookify.Common.Models;
using Bookify.Common.Repositories;
using Bookify.DataAccess.Repositories;

namespace Bookify.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Bookify.API.Controllers.BaseApiController" />
    [RoutePrefix("auth")]
    public class AuthController : BaseApiController
    {
        private readonly IAuthenticationRepository _authRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="authRepository">The authentication repository. This will be injected.</param>
        public AuthController(IAuthenticationRepository authRepository)
        {
            this._authRepository = authRepository;
        }

        /// <summary>
        /// Uses the specified command to login.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <response code="200" cref="Login">OK</response>
        /// <response code="500">Internal Server Error</response>
        /// <response code="400">Bad Request Error</response>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        [ResponseType(typeof(AuthTokenDto))]
        public async Task<IHttpActionResult> Login([FromBody]AuthenticateCommand command)
        {
            return await this.Try(async () => await this._authRepository.Login(command));
        }

        /// <summary>
        /// uses the specified command to register.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <response code="200" cref="Register">OK</response>
        /// <response code="500">Internal Server Error</response>
        /// <response code="400">Bad Request Error</response>
        /// <returns></returns>
        [HttpPost]
        [Route("register")]
        [ResponseType(typeof(AuthTokenDto))]
        public async Task<IHttpActionResult> Register([FromBody]CreateAccountCommand command)
        {
            return await this.Try(
                async () =>
                    {
                        var person = await this._authRepository.Register(command);
                        return await this._authRepository.Login(
                            new AuthenticateCommand { Email = command.Email, Password = command.Password });
                    });
        }
    }
}
