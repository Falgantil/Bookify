using System.Threading.Tasks;
using Bookify.App.Core.Collections;
using Bookify.Common.Filter;

namespace Bookify.App.Core.Interfaces.Services
{
    /// <summary>
    /// The Get By Filter service.
    /// Inherit from this to make a service usable in 
    /// <see cref="ObservableServiceCollection{TModel,TFilter,TService}"/>.
    /// </summary>
    /// <typeparam name="TDto">The type of the dto.</typeparam>
    /// <typeparam name="TFilter">The type of the filter.</typeparam>
    public interface IGetByFilterService<TDto, TFilter>
    {
        /// <summary>
        /// Gets the items, using the provided Filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        Task<IPaginatedEnumerable<TDto>> GetItems(TFilter filter);
    }
}