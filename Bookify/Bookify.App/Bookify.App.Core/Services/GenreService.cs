using System.Threading.Tasks;

using Bookify.App.Core.Interfaces.Services;
using Bookify.App.Sdk.Interfaces;
using Bookify.Common.Filter;
using Bookify.Common.Models;

namespace Bookify.App.Core.Services
{
    /// <summary>
    /// The Genre Service implementation.
    /// </summary>
    /// <seealso cref="IGenreService" />
    public class GenreService : IGenreService
    {
        /// <summary>
        /// The API
        /// </summary>
        private readonly IGenresApi api;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenreService"/> class.
        /// </summary>
        /// <param name="api">The API.</param>
        public GenreService(IGenresApi api)
        {
            this.api = api;
        }

        /// <summary>
        /// Gets the items, using the provided Filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public async Task<IPaginatedEnumerable<GenreDto>> GetItems(GenreFilter filter)
        {
            return await this.api.GetGenres(filter);
        }
    }
}