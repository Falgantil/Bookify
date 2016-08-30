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
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class BaseApiController : ApiController
    {

        /// <summary>
        /// Tries the specified operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="operation">The operation.</param>
        /// <returns></returns>
        protected async Task<IHttpActionResult> Try<T>(Func<Task<T>> operation)
        {
            return await this.TryRaw(async () =>
            {
                var content = await operation();
                var response = this.Ok(content);
                AddPaginationHeader(content as IPaginatedEnumerable);
                return response;
            });
        }

        /// <summary>
        /// Tries the specified operation.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <returns></returns>
        protected async Task<IHttpActionResult> Try(Func<Task> operation)
        {
            return await this.TryRaw(async () =>
            {
                await operation();
                return this.StatusCode(HttpStatusCode.NoContent);
            });
        }

        /// <summary>
        /// Tries the execute a create function.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="operation">The operation.</param>
        /// <returns></returns>
        protected async Task<IHttpActionResult> TryCreate<T>(Func<Task<T>> operation)
        {
            return await this.TryRaw(async () =>
            {
                var content = await operation();
                return this.Content(HttpStatusCode.Created, content);
            });
        }

        /// <summary>
        /// Tries the to excute the function raw.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <response code="500">Internal Server Error</response>
        /// <returns></returns>
        protected async Task<IHttpActionResult> TryRaw(Func<Task<IHttpActionResult>> operation)
        {
            try
            {
                return await operation();
            }
            catch (ApiException ex)
            {
                return this.Content((HttpStatusCode)ex.StatusCode, new
                {
                    ex.Message,
                    ex.StatusCode
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

        /// <summary>
        /// Adds a pagination header.
        /// </summary>
        /// <param name="paginated">The paginated.</param>
        private static void AddPaginationHeader(IPaginatedEnumerable paginated)
        {
            if (paginated != null)
            {
                const string headerTotalCount = "X-TotalCount";
                var response = HttpContext.Current.Response;
                response.Headers[headerTotalCount] = paginated.TotalCount.ToString();
            }
        }

        /// <summary>
        /// Gets the authorized member.
        /// </summary>
        /// <param name="authRepository"></param>
        /// <returns></returns>
        protected async Task<PersonAuthDto> GetAuthorizedMember(IAuthenticationRepository authRepository)
        {
            var token = this.Request.Headers.Authorization.Parameter;
            var personAuthDto = await authRepository.VerifyToken(token);
            return personAuthDto;
        }
    }
}