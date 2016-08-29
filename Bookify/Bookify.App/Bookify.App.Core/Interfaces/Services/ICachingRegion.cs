using System.Threading.Tasks;

namespace Bookify.App.Core.Interfaces.Services
{
    /// <summary>
    /// A caching region is a single file on the device that allows for persisting data on the disc.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICachingRegion<T>
    {
        /// <summary>
        /// Gets the item from the disc.
        /// </summary>
        /// <returns></returns>
        Task<T> GetItem();

        /// <summary>
        /// Updates the item on the disc. Accepts null as parameter, if <see cref="T"/> is nullable.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        Task UpdateItem(T item);
    }
}