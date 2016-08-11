using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Bookify.API.Controllers
{
    [Authorize]
    public class PublishersController : ApiController
    {
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            return Ok();
        }

        [HttpPut]
        public async Task<IHttpActionResult> Create()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IHttpActionResult> Update(int id)
        {
            return Ok();
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            return Ok();
        }
    }
}
