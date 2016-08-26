using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

using Bookify.Common.Exceptions;
using Bookify.Common.Filter;
using Bookify.Common.Models;
using Bookify.Common.Repositories;

namespace Bookify.API.Controllers
{
    public class BaseApiController : ApiController
    {
        public async Task<IHttpActionResult> Try<T>(Func<Task<T>> operation)
        {
            return await this.TryRaw(async () =>
            {
                var content = await operation();
                var response = this.Ok(content);
                this.AddPaginationHeader(content as IPaginatedEnumerable);
                return response;
            });
        }

        public async Task<IHttpActionResult> Try(Func<Task> operation)
        {
            return await this.TryRaw(async () =>
            {
                await operation();
                return this.StatusCode(HttpStatusCode.NoContent);
            });
        }

        public async Task<IHttpActionResult> TryCreate<T>(Func<Task<T>> operation)
        {
            return await this.TryRaw(async () =>
            {
                var content = await operation();
                return this.Content(HttpStatusCode.Created, content);
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
        
        private void AddPaginationHeader(IPaginatedEnumerable paginated)
        {
            if (paginated != null)
            {
                const string HeaderTotalCount = "X-TotalCount";
                var response = HttpContext.Current.Response;
                response.Headers[HeaderTotalCount] = paginated.TotalCount.ToString();
            }
        }

        protected async Task<PersonAuthDto> GetAuthorizedMember(IAuthenticationRepository authRepo)
        {
            var token = this.Request.Headers.Authorization.Parameter;
            var personAuthDto = await authRepo.VerifyToken(token);
            return personAuthDto;
        }
    }
}