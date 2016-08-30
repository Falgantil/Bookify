using System.Collections.Generic;
using System.Threading.Tasks;
using Bookify.App.Sdk.Interfaces;
using Bookify.Common.Filter;
using Bookify.Common.Models;

namespace Bookify.App.Sdk.Implementations
{
    public class GenresApi : BaseApi, IGenresApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenresApi"/> class.
        /// </summary>
        public GenresApi() : base(ApiConfig.GenresRoot)
        {

        }

        /// <summary>
        /// Gets the genres, filtered by <see cref="filter" />.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public async Task<IEnumerable<GenreDto>> GetGenres(GenreFilter filter)
        {
            var request = new RequestBuilder()
                .BaseUri(this.Url)
                .AddQuery(nameof(filter.Skip), filter.Skip)
                .AddQuery(nameof(filter.Take), filter.Take);
            if (!string.IsNullOrEmpty(filter.Search))
            {
                request.AddQuery(nameof(filter.Search), filter.Search);
            }
            return await this.ExecuteAndParse<GenreDto[]>(request);
        }
    }
}