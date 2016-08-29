using Bookify.App.Core.Interfaces.Services;

using PCLStorage;

namespace Bookify.App.Core.Services
{
    /// <summary>
    /// The Caching Region Factory implementation
    /// </summary>
    /// <seealso cref="ICachingRegionFactory" />
    public class CachingRegionFactory : ICachingRegionFactory
    {
        /// <summary>
        /// The file system
        /// </summary>
        private readonly IFileSystem fileSystem;

        /// <summary>
        /// Initializes a new instance of the <see cref="CachingRegionFactory"/> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        public CachingRegionFactory(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        /// <summary>
        /// Creates a new caching region with the specified region name (will create a file called '<see cref="regionName" />.dat').
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="regionName">Name of the region.</param>
        /// <returns></returns>
        public ICachingRegion<T> CreateRegion<T>(string regionName)
        {
            return new JsonCachingRegion<T>(regionName, this.fileSystem);
        }
    }
}