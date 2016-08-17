using System;
using System.IO;
using Bookify.Core.Interfaces.Repositories;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.File;

namespace Bookify.DataAccess.Repositories
{
    public class AzureFileServerRepository : IFileServerRepository
    {
        private const string _share = "bookifyshare";
        private const string _covers = "Covers";
        private const string _epubs = "Epubs";

        readonly CloudStorageAccount _storageAccount =
            CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

        private CloudFileShare ConnectionToAzureShare()
        {
            CloudFileClient fileClient = _storageAccount.CreateCloudFileClient();
            CloudFileShare share = fileClient.GetShareReference(_share);
            return !share.Exists() ? null : share;
        }

        private CloudFileDirectory ConnectToDirectory(CloudFileShare share, string dir)
        {
            CloudFileDirectory rootDirectory = share.GetRootDirectoryReference();
            CloudFileDirectory epubsDirectory = rootDirectory.GetDirectoryReference(dir);
            return !epubsDirectory.Exists() ? null : epubsDirectory;
        }

        public void SaveEpubFile(int bookId, Stream file)
        {
            
        }

        public void ReplaceEpubFile(int bookId, Stream file)
        {
            DeleteEpubFile(bookId);
            SaveEpubFile(bookId, file);
        }

        public void DeleteEpubFile(int bookId)
        {
        }

        public void SaveCoverFile(int bookId, Stream file)
        {
        }

        public void ReplaceCoverFile(int bookId, Stream file)
        {
            DeleteCoverFile(bookId);
            SaveCoverFile(bookId, file);
        }

        public void DeleteCoverFile(int bookId)
        {
        }

        public MemoryStream GetCoverFile(int bookId)
        {
            var share = ConnectionToAzureShare();
            if (share == null) return null;
            var epubsDirectory = ConnectToDirectory(share, _covers);
            if (epubsDirectory == null) return null;


            var epubFile = epubsDirectory.GetFileReference($"{bookId}.png");
            if (!epubFile.Exists())
                return null;

            var stream = new MemoryStream();
            epubFile.DownloadToStream(stream);
            return stream;
        }
    }
}