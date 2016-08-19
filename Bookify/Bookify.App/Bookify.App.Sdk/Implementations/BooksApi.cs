using System.Collections.Generic;
using System.Threading.Tasks;
using Bookify.App.Sdk.Interfaces;
using Bookify.Common.Filter;
using Bookify.Common.Models;
using Newtonsoft.Json;

namespace Bookify.App.Sdk.Implementations
{
    public class BooksApi : BaseApi, IBooksApi
    {
        public BooksApi()
            : base(ApiConfig.BooksRoot)
        {
        }

        public async Task<IPaginatedEnumerable<BookDto>> GetItems(BookFilter filter)
        {
            var request = new RequestBuilder()
                .BaseUri(this.Url)
                .AddQuery(nameof(filter.Index), filter.Index)
                .AddQuery(nameof(filter.Count), filter.Count);

            if (!string.IsNullOrEmpty(filter.SearchText))
            {
                request.AddQuery(nameof(filter.SearchText), filter.SearchText);
            }

            if (filter.GenreIds != null)
            {
                foreach (var genreId in filter.GenreIds)
                {
                    request.AddQuery(nameof(filter.GenreIds), genreId);
                }
            }
            
            return await this.ExecuteAndParse<PaginatedEnumerable<BookDto>>(request);
        }

        public async Task<DetailedBookDto> Get(int id)
        {
            var request = new RequestBuilder()
                .BaseUri(this.CombineUrl("{id}"))
                .AddUriSegment("id", id);

            var response = await this.ExecuteRequest(request);
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<DetailedBookDto>(json);
        }
    }
}