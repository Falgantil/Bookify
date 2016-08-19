using System;
using System.IO;
using System.Threading.Tasks;
using Bookify.Common.Repositories;

namespace Bookify.DataAccess.Repositories
{
    public class WindowsFileServerRepository : IFileServerRepository
    {
        public Task SaveEpubFile(int bookId, Stream source)
        {
            throw new System.NotImplementedException();
        }

        public Task ReplaceEpubFile(int bookId, Stream source)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteEpubFile(int bookId)
        {
            throw new System.NotImplementedException();
        }

        public Task GetEpubFile(int bookId)
        {
            throw new System.NotImplementedException();
        }

        public Task SaveCoverFile(int bookId, Stream source)
        {
            throw new System.NotImplementedException();
        }

        public Task ReplaceCoverFile(int bookId, Stream source)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteCoverFile(int bookId)
        {
            throw new System.NotImplementedException();
        }

        public Task GetCoverFile(int bookId)
        {
            throw new System.NotImplementedException();
        }

        Task<MemoryStream> IFileServerRepository.GetEpubFile(int bookId)
        {
            throw new NotImplementedException();
        }

        Task<MemoryStream> IFileServerRepository.GetCoverFile(int bookId)
        {
            throw new NotImplementedException();
        }
    }
}