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

        public async Task<IPaginatedEnumerable<GenreDto>> GetItems(GenreFilter filter)
        {
            var request = new RequestBuilder()
                .BaseUri(this.Url)
                .AddQuery(nameof(filter.Index), filter.Index)
                .AddQuery(nameof(filter.Count), filter.Count);
            if (!string.IsNullOrEmpty(filter.SearchText))
            {
                request.AddQuery(nameof(filter.SearchText), filter.SearchText);
            }
            return await this.ExecuteAndParse<PaginatedEnumerable<GenreDto>>(request);
        }
    }
}