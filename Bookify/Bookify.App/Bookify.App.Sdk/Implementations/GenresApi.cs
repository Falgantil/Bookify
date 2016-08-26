using System.Collections.Generic;
using System.Threading.Tasks;
using Bookify.App.Sdk.Interfaces;
using Bookify.Common.Filter;
using Bookify.Common.Models;
using Newtonsoft.Json;

namespace Bookify.App.Sdk.Implementations
{
    public class GenresApi : BaseApi, IGenresApi
    {
        public GenresApi() : base(ApiConfig.GenresRoot)
        {

        }

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