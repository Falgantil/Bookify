using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using Bookify.Common.Exceptions;
using Bookify.Common.Repositories;

namespace Bookify.DataAccess.Repositories
{
    public class WindowsFileServerRepository : IFileServerRepository
    {
        private const string Share = "bookifyshare";
        private const string Covers = "Covers";
        private const string Epubs = "Epubs";


        private async Task SaveFile(int bookId, string folderName, Stream source, bool overWrite = false)
        {
            var filename = folderName == Covers ? $"{bookId}.png" : $"{bookId}.epub";
            string filePath = FilePath(ConfigurationManager.AppSettings["WindowsStorageConnectionString"], Share, folderName, filename);

            var exists = File.Exists(filePath);

            if (exists && overWrite)
            {
                using (var output = File.Open(filePath, FileMode.Create))
                {
                    await source.CopyToAsync(output);
                }
            }
            else if (!exists)
            {
                using (var output = File.Open(filePath, FileMode.CreateNew))
                {
                    await source.CopyToAsync(output);
                }
            }
            else
            {
                throw new FileAlreadyExistsException();
            }
        }

        private async Task DeleteFile(int bookId, string folderName)
        {
            var filename = folderName == Covers ? $"{bookId}.png" : $"{bookId}.epub";
            string filePath = FilePath(ConfigurationManager.AppSettings["WindowsStorageConnectionString"], Share, folderName, filename);
            

            var exists = File.Exists(filePath);

            if (exists)
            {
                await Task.Factory.StartNew(() => File.Delete(filePath));
            }
        }


        private async Task<MemoryStream> DownloadToStream(int bookId, string folderName)
        {
            var filename = folderName == Covers ? $"{bookId}.png" : $"{bookId}.epub";
            string filePath = FilePath(ConfigurationManager.AppSettings["WindowsStorageConnectionString"], Share, folderName, filename);

            var exists = File.Exists(filePath);
            if (!exists)
                throw new NotFoundException();
            
            return new MemoryStream(File.ReadAllBytes(filePath)); 
        }


        private string FilePath(string computername, string sharename, string foldername, string filename)
        {
            return new Uri(@"\\" + Path.Combine(computername, sharename, foldername, filename).Trim('\\')).LocalPath;
        }


        public async Task SaveEpubFile(int bookId, Stream source)
        {
            await SaveFile(bookId, Epubs, source);
        }

        public async Task ReplaceEpubFile(int bookId, Stream source)
        {
            await SaveFile(bookId, Epubs, source, true);
        }

        public async Task DeleteEpubFile(int bookId)
        {
            await DeleteFile(bookId, Epubs);
        }

        public async Task<MemoryStream> GetEpubFile(int bookId)
        {
            return await DownloadToStream(bookId, Epubs);
        }

        public async Task SaveCoverFile(int bookId, Stream source)
        {
            await SaveFile(bookId, Covers, source);
        }

        public async Task ReplaceCoverFile(int bookId, Stream source)
        {
            await SaveFile(bookId, Covers, source, true);
        }

        public async Task DeleteCoverFile(int bookId)
        {
            await DeleteFile(bookId, Covers);
        }

        public async Task<MemoryStream> GetCoverFile(int bookId)
        {
            return await DownloadToStream(bookId, Covers);
        }
    }
}