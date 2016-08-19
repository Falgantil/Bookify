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


        //public Task<IHttpActionResult> Try<T>(Task<T> operation)
        //{
        //    return this.Try(() => operation);
        //}
    }
}