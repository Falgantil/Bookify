using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;

using Bookify.Common.Commands.Auth;
using Bookify.Common.Exceptions;
using Bookify.Common.Repositories;
using Bookify.DataAccess.Repositories;

namespace Bookify.API.Controllers
{
    [RoutePrefix("auth")]
    public class AuthController : BaseApiController
    {
        private readonly IAuthenticationRepository authRepository;

        public AuthController(IAuthenticationRepository authRepository)
        {
            this.authRepository = authRepository;
        }

        [HttpPost]
        [Route("login")]
        [Authorize]
        public async Task<IHttpActionResult> Login([FromBody]AuthenticateCommand command)
        {
            return await this.Try(async () => await this.authRepository.Login(command));
        }

        [HttpPost]
        [Route("register")]
        public async Task<IHttpActionResult> Register([FromBody]CreateAccountCommand command)
        {
            return await this.Try(
                async () =>
                    {
                        var person = await this.authRepository.Register(command);
                        return
                            this.authRepository.Login(
                                new AuthenticateCommand { Email = command.Email, Password = command.Password });
                    });
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IHttpActionResult> Logout()
        {
            return this.Ok();
        }
    }
}
