using System.Threading.Tasks;
using Bookify.App.Sdk.Interfaces;

namespace Bookify.App.Sdk.Implementations
{
    public class FilesApi : BaseApi, IFilesApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilesApi"/> class.
        /// </summary>
        public FilesApi() 
            : base(ApiConfig.FilesRoot)
        {
        }

        /// <summary>
        /// Downloads the book based on the specified <see cref="bookId" />, as a complete byte array.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <returns></returns>
        public async Task<byte[]> DownloadBook(int bookId)
        {
            var request = new RequestBuilder()
                .BaseUri(this.CombineUrl("{id}", "epub"))
                .AddUriSegment("id", bookId);
            var response = await this.ExecuteRequest(request);
            return await response.Content.ReadAsByteArrayAsync();
        }
    }
}