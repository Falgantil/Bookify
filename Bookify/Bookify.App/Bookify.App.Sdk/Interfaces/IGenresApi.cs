using System.Collections.Generic;
using System.Threading.Tasks;
using Bookify.Common.Filter;
using Bookify.Common.Models;

namespace Bookify.App.Sdk.Interfaces
{
    public interface IGenresApi
    {
        /// <summary>
        /// Gets the genres, filtered by <see cref="filter"/>.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        Task<IEnumerable<GenreDto>> GetGenres(GenreFilter filter);
    }
}