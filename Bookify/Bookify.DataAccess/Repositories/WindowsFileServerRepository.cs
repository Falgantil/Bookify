using System.IO;
using Bookify.Core.Interfaces.Repositories;

namespace Bookify.DataAccess.Repositories
{
    public class WindowsFileServerRepository : IFileServerRepository
    {
        public void SaveEpubFile(int bookId, Stream file)
        {
            throw new System.NotImplementedException();
        }

        public void ReplaceEpubFile(int bookId, Stream file)
        {
            DeleteEpubFile(bookId);
            SaveEpubFile(bookId, file);
        }

        public void DeleteEpubFile(int bookId)
        {
            throw new System.NotImplementedException();
        }

        public void SaveCoverFile(int bookId, Stream file)
        {
            throw new System.NotImplementedException();
        }

        public void ReplaceCoverFile(int bookId, Stream file)
        {
            DeleteCoverFile(bookId);
            SaveCoverFile(bookId, file);
        }

        public void DeleteCoverFile(int bookId)
        {
            throw new System.NotImplementedException();
        }
    }
}