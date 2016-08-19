using System.Threading.Tasks;
using Bookify.Common.Filter;
using Bookify.Common.Models;

namespace Bookify.App.Sdk.Interfaces
{
    public interface IGenresApi : IGetByFilterApi<GenreDto, GenreFilter>
    {
    }

    public interface IGetByFilterApi<TDto, TFilter>
    {
        Task<IPaginatedEnumerable<TDto>> GetItems(TFilter filter);
    }
}