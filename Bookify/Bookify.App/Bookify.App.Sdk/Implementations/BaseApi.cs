using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

using Bookify.App.Sdk.Exceptions;
using Bookify.Common.Filter;
using ModernHttpClient;
using Newtonsoft.Json;

namespace Bookify.App.Sdk.Implementations
{
    public abstract class BaseApi
    {
        /// <summary>
        /// A dictionary that defines all the headers that will automatically be included in any API call.
        /// </summary>
        protected static readonly IDictionary<string, object> DefaultHeaders = new Dictionary<string, object>();

        /// <summary>
        /// Gets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        protected string Url { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseApi"/> class.
        /// </summary>
        /// <param name="url">The URL.</param>
        protected BaseApi(string url)
        {
            this.Url = url;
        }

        /// <summary>
        /// Combines the URL with <see cref="Url"/>.
        /// </summary>
        /// <param name="urls">The urls.</param>
        /// <returns></returns>
        protected string CombineUrl(params string[] urls)
        {
            List<string> paths = new List<string>
            {
                this.Url
            };
            paths.AddRange(urls);
            return Path.Combine(paths.ToArray()).TrimEnd('/');
        }

        /// <summary>
        /// Executes the request, returning a <see cref="HttpResponseMessage"/>.
        /// If the response is not successful, it will instead throw an exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        protected virtual async Task<HttpResponseMessage> ExecuteRequest(RequestBuilder message)
        {
            using (var client = new HttpClient(new NativeMessageHandler()))
            {
                message.AddHeaders(DefaultHeaders);

                var httpRequestMessage = message.ToHttpRequestMessage();

                var response = await client.SendAsync(httpRequestMessage);
                await ThrowIfUnsuccessful(response);
                return response;
            }
        }

        /// <summary>
        /// Executes the request using <see cref="ExecuteRequest"/> and parses the response as JSON to the type of <see cref="T"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        protected virtual async Task<T> ExecuteAndParse<T>(RequestBuilder message)
        {
            var response = await this.ExecuteRequest(message);
            var json = await response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<T>(json);
            var paginated = obj as IPaginatedEnumerable;
            if (paginated != null)
            {
                const string HeaderTotalCount = "X-TotalCount";
                foreach (var strCount in response.Headers.GetValues(HeaderTotalCount))
                {
                    int count;
                    if (!int.TryParse(strCount, out count))
                    {
                        continue;
                    }
                    paginated.TotalCount += count;
                }
            }
            return obj;
        }

        /// <summary>
        /// Throws if the <see cref="message"/>'s Status Code is unsuccessful (not 200OK or a variant thereof).
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        private static async Task<HttpResponseMessage> ThrowIfUnsuccessful(HttpResponseMessage message)
        {
            if (message.IsSuccessStatusCode)
            {
                return message;
            }
            throw await HttpResponseException.CreateException(message);
        }
    }
}