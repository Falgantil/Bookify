using System.Threading.Tasks;
using Bookify.App.Sdk.Interfaces;

namespace Bookify.App.Sdk.Implementations
{
    public class FilesApi : BaseApi, IFilesApi
    {
        public FilesApi() 
            : base(ApiConfig.FilesRoot)
        {
        }

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