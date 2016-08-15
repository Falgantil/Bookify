using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

using Bookify.App.Sdk.Exceptions;
using Bookify.Models;

using ModernHttpClient;

using Newtonsoft.Json;

namespace Bookify.App.Sdk
{
    public abstract class BaseApi
    {
        protected string FullUrl { get; private set; }

        protected BaseApi(string url)
        {
            this.FullUrl = Path.Combine(AppConfig.Website, url);
        }

        protected string CombineUrl(string url)
        {
            return Path.Combine(this.FullUrl, url);
        }

        public virtual async Task<HttpResponseMessage> ExecuteRequest(HttpRequestMessage message)
        {
            using (var client = new HttpClient(new NativeMessageHandler()))
            {
                var response = await client.SendAsync(message);
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

    public class BookApi : BaseApi
    {
        public BookApi()
            : base("books")
        {
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, this.FullUrl);
            var response = await this.ExecuteRequest(request);
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<Book>>(json);
        }
    }
}