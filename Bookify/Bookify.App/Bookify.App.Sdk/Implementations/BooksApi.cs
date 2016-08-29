using System.Threading.Tasks;
using Bookify.App.Sdk.Interfaces;
using Bookify.Common.Filter;
using Bookify.Common.Models;
using Newtonsoft.Json;

namespace Bookify.App.Sdk.Implementations
{
    public class BooksApi : BaseApi, IBooksApi
    {
        public async Task<DetailedBookDto> Get(int id)
        {
            var request = new RequestBuilder()
                .BaseUri(this.CombineUrl("{id}"))
                .AddUriSegment("id", id);

            var response = await this.ExecuteRequest(request);
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<DetailedBookDto>(json);
        }

        public BooksApi()
            : base(ApiConfig.BooksRoot)
        {
        }

        public async Task<IPaginatedEnumerable<BookDto>> GetBooks(BookFilter filter)
        {
            var request = new RequestBuilder()
                .BaseUri(this.Url)
                .AddQuery(nameof(filter.Skip), filter.Skip)
                .AddQuery(nameof(filter.Take), filter.Take);

            if (!string.IsNullOrEmpty(filter.Search))
            {
                request.AddQuery(nameof(filter.Search), filter.Search);
            }

            if (filter.Genres != null)
            {
                foreach (var genreId in filter.Genres)
                {
                    request.AddQuery(nameof(filter.Genres), genreId);
                }
            }
            
            return await this.ExecuteAndParse<PaginatedEnumerable<BookDto>>(request);
        }

        public async Task<IPaginatedEnumerable<BookDto>> GetMyBooks(BookFilter filter)
        {
            var request = new RequestBuilder()
                .BaseUri(this.CombineUrl("mybooks"))
                .AddQuery(nameof(filter.Skip), filter.Skip)
                .AddQuery(nameof(filter.Take), filter.Take);

            if (!string.IsNullOrEmpty(filter.Search))
            {
                request.AddQuery(nameof(filter.Search), filter.Search);
            }

            if (filter.Genres != null)
            {
                foreach (var genreId in filter.Genres)
                {
                    request.AddQuery(nameof(filter.Genres), genreId);
                }
            }

            return await this.ExecuteAndParse<PaginatedEnumerable<BookDto>>(request);
        }
    }
}