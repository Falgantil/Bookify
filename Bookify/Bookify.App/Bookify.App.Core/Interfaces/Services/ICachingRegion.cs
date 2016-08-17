using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PCLStorage;

namespace Bookify.App.Core.Interfaces.Services
{
    public interface ICachingRegion<T>
    {
        Task<T> GetItem();

        Task UpdateItem(T item);
    }

    public class JsonCachingRegion<T> : ICachingRegion<T>
    {
        private readonly string regionName;

        private readonly IFileSystem fileSystem;

        public JsonCachingRegion(string regionName, IFileSystem fileSystem)
        {
            this.regionName = regionName + ".dat";
            this.fileSystem = fileSystem;
        }

        private async Task<IFile> GetFile()
        {
            var folder = await this.fileSystem.LocalStorage.CreateFolderAsync("Storage", CreationCollisionOption.OpenIfExists);
            var file = await folder.CreateFileAsync(this.regionName, CreationCollisionOption.OpenIfExists);
            return file;
        }

        public async Task<T> GetItem()
        {
            var file = await this.GetFile();
            var content = await file.ReadAllTextAsync();
            if (string.IsNullOrEmpty(content))
            {
                return default(T);
            }
            try
            {
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch (JsonSerializationException)
            {
                return default(T);
            }
        }

        public async Task UpdateItem(T item)
        {
            var file = await this.GetFile();

            var json = string.Empty;
            if (item != null)
            {
                json = JsonConvert.SerializeObject(item);
            }
            await file.WriteAllTextAsync(json);
        }
    }
}