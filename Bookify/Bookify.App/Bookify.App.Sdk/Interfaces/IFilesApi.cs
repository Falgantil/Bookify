using System.Threading.Tasks;

namespace Bookify.App.Sdk.Interfaces
{
    public interface IFilesApi
    {
        Task<byte[]> DownloadBook(int bookId);
    }
}