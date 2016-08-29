using System.Threading.Tasks;
using System.Web.Http;
using Bookify.DataAccess.Repositories;

namespace Bookify.API.Controllers
{
    [RoutePrefix("brewer")]
    public class ExampleController : BaseApiController
    {
        // local brewer API
        private readonly IBrewerRepository _brewerRepository;

        // When someone GETs a coffee, create connection
        public ExampleController(IBrewerRepository brewerRepository)
        {
            _brewerRepository = brewerRepository;
        }

        // This action makes the brewer brew a cup of coffee. (result may vary)
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetCoffeeAction()
        {
            return await Try(async () =>
            {
                var c = await this._brewerRepository.GetCoffee();
                return c.IsItGood();
            });
        }
    }
}
