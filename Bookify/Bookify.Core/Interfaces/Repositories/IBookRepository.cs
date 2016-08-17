using System.Linq;
using System.Threading.Tasks;
using Bookify.Models;

namespace Bookify.Core.Interfaces.Repositories
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<IQueryable<Book>> GetAllByParams(int? skip, int? take, int[] genres, string search, string orderBy, bool? desc);
        Task<Book> FindWithContent(int id);
        Task<Book> FindForStatistics(int id);
    }
}
