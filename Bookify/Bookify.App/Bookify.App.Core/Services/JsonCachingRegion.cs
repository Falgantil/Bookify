using System.Threading.Tasks;

using Bookify.App.Core.Interfaces.Services;

using Newtonsoft.Json;

using PCLStorage;

namespace Bookify.App.Core.Services
{
    public class JsonCachingRegion<T> : ICachingRegion<T>
    {
        /// <summary>
        /// The region name
        /// </summary>
        private readonly string regionName;

        /// <summary>
        /// The file system
        /// </summary>
        private readonly IFileSystem fileSystem;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonCachingRegion{T}"/> class.
        /// </summary>
        /// <param name="regionName">Name of the region.</param>
        /// <param name="fileSystem">The file system.</param>
        public JsonCachingRegion(string regionName, IFileSystem fileSystem)
        {
            this.regionName = regionName + ".dat";
            this.fileSystem = fileSystem;
        }

        /// <summary>
        /// Gets the item from the disc.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Updates the item on the disc. Accepts null as parameter, if <see cref="!:T" /> is nullable.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the file from the disc, creating it if it does not already exist.
        /// </summary>
        /// <returns></returns>
        private async Task<IFile> GetFile()
        {
            var folder = await this.fileSystem.LocalStorage.CreateFolderAsync("Storage", CreationCollisionOption.OpenIfExists);
            var file = await folder.CreateFileAsync(this.regionName, CreationCollisionOption.OpenIfExists);
            return file;
        }
    }
}