using System.Threading.Tasks;
using Bookify.Common.Filter;
using Bookify.Common.Models;

namespace Bookify.App.Sdk.Interfaces
{
    public interface IGenresApi
    {
        Task<IPaginatedEnumerable<GenreDto>> GetGenres(GenreFilter filter);
    }
}