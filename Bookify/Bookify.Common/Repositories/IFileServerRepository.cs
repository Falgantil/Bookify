using System.IO;
using System.Threading.Tasks;

namespace Bookify.Common.Repositories
{
    public interface IFileServerRepository
    {
        Task SaveEpubFile(int bookId, Stream source);
        Task ReplaceEpubFile(int bookId, Stream source);
        Task DeleteEpubFile(int bookId);
        Task<MemoryStream> GetEpubFile(int bookId);

        Task SaveCoverFile(int bookId, Stream source);
        Task ReplaceCoverFile(int bookId, Stream source);
        Task DeleteCoverFile(int bookId);
        Task<MemoryStream> GetCoverFile(int bookId);

    }
}
