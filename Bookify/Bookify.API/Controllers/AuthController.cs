using System.Threading.Tasks;
using System.Web.Http;
using Bookify.Core;

namespace Bookify.API.Controllers
{
    public class AuthController : ApiController
    {
        private IAuthenticationService _authService;

        public AuthController(IAuthenticationService authService)
        {
            this._authService = authService;
        }

        [HttpPost]
        public async Task<IHttpActionResult> Login()
        {

            return Ok();
        }

        [HttpGet]
        [Authorize]
        public async Task<IHttpActionResult> Logout()
        {
            return Ok();
        }
    }
}
