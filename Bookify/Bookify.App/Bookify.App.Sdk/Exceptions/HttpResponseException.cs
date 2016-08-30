using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Bookify.App.Sdk.Exceptions
{
    public class HttpResponseException : WebException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpResponseException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        private HttpResponseException(HttpResponseMessage message)
            : base(message.ReasonPhrase)
        {
            this.StatusCode = message.StatusCode;
            this.Reason = message.ReasonPhrase;
        }

        /// <summary>
        /// Gets the exception status code.
        /// </summary>
        /// <value>
        /// The status code.
        /// </value>
        public HttpStatusCode StatusCode { get; private set; }

        /// <summary>
        /// Gets the reason that was returned in the unsuccessful HTTP request.
        /// </summary>
        /// <value>
        /// The reason.
        /// </value>
        public string Reason { get; private set; }

        /// <summary>
        /// Gets the JSON content that was returned in the unsuccessful HTTP request.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public string Content { get; private set; }

        /// <summary>
        /// Creates a new instance of the <see cref="HttpResponseException"/> exception, based on the <see cref="message"/>.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
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