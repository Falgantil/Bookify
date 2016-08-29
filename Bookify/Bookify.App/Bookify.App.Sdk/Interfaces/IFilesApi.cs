using System.Threading.Tasks;

namespace Bookify.App.Sdk.Interfaces
{
    public interface IFilesApi
    {
        /// <summary>
        /// Downloads the book based on the specified <see cref="bookId"/>, as a complete byte array.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <returns></returns>
        Task<byte[]> DownloadBook(int bookId);
    }
}