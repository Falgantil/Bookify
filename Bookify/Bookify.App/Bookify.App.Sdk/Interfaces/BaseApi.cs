using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Bookify.App.Sdk.Exceptions;
using ModernHttpClient;

namespace Bookify.App.Sdk.Interfaces
{
    public abstract class BaseApi
    {
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
                var httpRequestMessage = message.ToHttpRequestMessage();

                var response = await client.SendAsync(httpRequestMessage);
                await ThrowIfUnsuccessful(response);
                return response;
            }
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