using System.Collections.Generic;
using System.Threading.Tasks;
using Bookify.Models;
using System.Linq;

namespace Bookify.Core.Interfaces
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<IQueryable<Book>> GetAllByParams(int? skip, int? take, int[] genres, string search, string orderBy, bool? desc);
    }
}
