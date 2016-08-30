using System;
using System.IO;
using System.Threading.Tasks;
using Bookify.Common.Exceptions;
using Bookify.Common.Repositories;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.File;

namespace Bookify.DataAccess.Repositories
{
    public class AzureFileServerRepository : IFileServerRepository
    {
        private const string Share = "bookifyshare";
        private const string Covers = "Covers";
        private const string Epubs = "Epubs";

        private readonly CloudStorageAccount _storageAccount =
            CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

        private CloudFileShare ConnectionToAzureShare()
        {
            CloudFileClient fileClient = _storageAccount.CreateCloudFileClient();
            CloudFileShare share = fileClient.GetShareReference(Share);
            return !share.Exists() ? null : share;
        }

        private CloudFileDirectory ConnectToDirectory(CloudFileShare share, string dir)
        {
            CloudFileDirectory rootDirectory = share.GetRootDirectoryReference();
            CloudFileDirectory epubsDirectory = rootDirectory.GetDirectoryReference(dir);
            return !epubsDirectory.Exists() ? null : epubsDirectory;
        }

        private async Task DeleteFile(int bookId, string folderName)
        {
            var share = ConnectionToAzureShare();
            if (share == null) return;
            var directory = ConnectToDirectory(share, folderName);
            if (directory == null) return;

            var filename = folderName == Covers ? $"{bookId}.png" : $"{bookId}.epub";
            var file = directory.GetFileReference(filename);
            await file.DeleteIfExistsAsync();
        }

        private async Task SaveFile(int bookId, string folderName, Stream source,  bool overWrite = false)
        {
            var share = ConnectionToAzureShare();
            if (share == null) throw new NullReferenceException();
            
            var directory = ConnectToDirectory(share, folderName);
            if (directory == null) throw new NullReferenceException(); ;

            var filename = folderName == Covers ? $"{bookId}.png" : $"{bookId}.epub";
            var file = directory.GetFileReference(filename);
            if (!file.Exists() || overWrite)
            {
                await file.UploadFromStreamAsync(source);
            }
            else
            {
                throw new FileAlreadyExistsException();
            }
        }

        private async Task<MemoryStream> DownloadToStream(int bookId, string folderName)
        {
            var share = ConnectionToAzureShare();
            if (share == null) return null;
            var directory = ConnectToDirectory(share, folderName);
            if (directory == null) return null;


            var filename = folderName == Covers ? $"{bookId}.png" : $"{bookId}.epub";
            var file = directory.GetFileReference(filename);
            if (!file.Exists())
                return null;

            var stream = new MemoryStream();
            await file.DownloadToStreamAsync(stream);
            return stream;
        }

        #region interface implementation

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

        #endregion
    }
}