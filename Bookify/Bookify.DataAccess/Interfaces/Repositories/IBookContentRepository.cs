using System.Threading.Tasks;

using Bookify.DataAccess.Models;

namespace Bookify.DataAccess.Interfaces.Repositories
{
    public interface IBookContentRepository : IGenericRepository<BookContent>
    {
        Task<BookContent> GetById(int id);
    }
}
