namespace Bookify.App.Core.Interfaces.Services
{
    /// <summary>
    /// The Caching Region factory. Responsible for creating new Caching Region instances.
    /// </summary>
    public interface ICachingRegionFactory
    {
        /// <summary>
        /// Creates a new caching region with the specified region name (will create a file called '<see cref="regionName"/>.dat').
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="regionName">Name of the region.</param>
        /// <returns></returns>
        ICachingRegion<T> CreateRegion<T>(string regionName);
    }
}