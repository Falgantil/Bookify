using System.Threading.Tasks;
using Bookify.Models;
using System.Linq;
using Bookify.Models.ViewModels;

namespace Bookify.Core.Interfaces.Repositories
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<BookSearch> GetAllByParams(int? skip, int? take, int[] genres, string search, string orderBy, bool? desc);
        //Task<Book> FindWithContent(int id);
        Task<Book> FindForStatistics(int id);
    }
}
