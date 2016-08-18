using System.Threading.Tasks;

namespace Bookify.App.Core.Interfaces.Services
{
    public interface ICachingRegion<T>
    {
        Task<T> GetItem();

        Task UpdateItem(T item);
    }
}