using Bookify.Models;

namespace Bookify.Core
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        void Disable(int id);
    }
}
