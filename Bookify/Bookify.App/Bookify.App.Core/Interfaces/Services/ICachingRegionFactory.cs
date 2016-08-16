using PCLStorage;

namespace Bookify.App.Core.Interfaces.Services
{
    public interface ICachingRegionFactory
    {
        ICachingRegion<T> CreateRegion<T>(string regionName);
    }

    public class CachingRegionFactory : ICachingRegionFactory
    {
        private readonly IFileSystem fileSystem;

        public CachingRegionFactory(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        public ICachingRegion<T> CreateRegion<T>(string regionName)
        {
            return new JsonCachingRegion<T>(regionName, this.fileSystem);
        }
    }
}