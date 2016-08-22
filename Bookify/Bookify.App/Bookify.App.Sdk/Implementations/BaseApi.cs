using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        protected static readonly IDictionary<string, object> DefaultHeaders = new Dictionary<string, object>();

        protected string Url { get; private set; }

        protected BaseApi(string url)
        {
            this.Url = url;
        }

        protected string CombineUrl(string url)
        {
            return Path.Combine(this.Url, url);
        }

        public virtual async Task<HttpResponseMessage> ExecuteRequest(RequestBuilder message)
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

        public virtual async Task<T> ExecuteAndParse<T>(RequestBuilder message)
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