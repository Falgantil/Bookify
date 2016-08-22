using System.Threading.Tasks;

using Bookify.Common.Filter;

namespace Bookify.App.Core.Interfaces.Services
{
    public interface IGetByFilterService<TDto, TFilter>
    {
        Task<IPaginatedEnumerable<TDto>> GetItems(TFilter filter);
    }
}