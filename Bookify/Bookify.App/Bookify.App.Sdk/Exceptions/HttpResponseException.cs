using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Bookify.App.Sdk.Exceptions
{
    public class HttpResponseException : WebException
    {
        private HttpResponseException(HttpResponseMessage message)
            : base(message.ReasonPhrase)
        {
            this.StatusCode = message.StatusCode;
            this.Reason = message.ReasonPhrase;
        }

        public HttpStatusCode StatusCode { get; private set; }

        public string Reason { get; private set; }

        public string Content { get; private set; }

        public static async Task<HttpResponseException> CreateException(HttpResponseMessage message)
        {
            var content = message.Content != null ? await message.Content.ReadAsStringAsync() : null;
            var httpResponseException = new HttpResponseException(message)
            {
                Content = content
            };
            return httpResponseException;
        }
    }
}