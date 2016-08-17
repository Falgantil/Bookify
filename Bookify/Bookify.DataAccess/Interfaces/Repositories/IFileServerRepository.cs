using System.IO;

namespace Bookify.DataAccess.Interfaces.Repositories
{
    public interface IFileServerRepository
    {
        void SaveEpubFile(int bookId, Stream file);
        void ReplaceEpubFile(int bookId, Stream file);
        void DeleteEpubFile(int bookId);


        void SaveCoverFile(int bookId, Stream file);
        void ReplaceCoverFile(int bookId, Stream file);
        void DeleteCoverFile(int bookId);

        MemoryStream GetCoverFile(int bookId);

    }
}
