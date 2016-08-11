using System.Threading.Tasks;
using System.Web.Http;

namespace Bookify.API.Controllers
{
    public class AuthController : ApiController
    {

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
