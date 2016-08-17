using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

using Bookify.Common.Exceptions;

namespace Bookify.API.Controllers
{
    public class BaseApiController : ApiController
    {
        public async Task<IHttpActionResult> Try<T>(Func<Task<T>> operation)
        {
            return await this.TryRaw(async () =>
            {
                var content = await operation();
                return this.Ok(content);
            });
        }

        public async Task<IHttpActionResult> Try(Func<Task> operation)
        {
            return await this.TryRaw(async () =>
            {
                await operation();
                return this.Ok();
            });
        }

        public async Task<IHttpActionResult> TryRaw(Func<Task<IHttpActionResult>> operation)
        {
            try
            {
                return await operation();
            }
            catch (NotFoundException ex)
            {
                return this.Content(HttpStatusCode.NotFound, new { Message = ex.Message });
            }
            catch (AuthenticationRequiredException ex)
            {
                return this.Content(HttpStatusCode.Forbidden, new { Message = ex.Message });
            }
            catch (Exception)
            {
                return this.Content(
                    HttpStatusCode.InternalServerError,
                    new { Message = "An unknown error occurred on the server." });
            }
        }


        //public Task<IHttpActionResult> Try<T>(Task<T> operation)
        //{
        //    return this.Try(() => operation);
        //}
    }
}