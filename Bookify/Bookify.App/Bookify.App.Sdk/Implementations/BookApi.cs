using System.Collections.Generic;
using System.Threading.Tasks;
using Bookify.App.Sdk.Interfaces;
using Bookify.Common.Filter;
using Bookify.Common.Models;
using Newtonsoft.Json;

namespace Bookify.App.Sdk.Implementations
{
    public class BookApi : BaseApi, IBookApi
    {
        public BookApi()
            : base(AppConfig.BooksRoot)
        {
        }

        public async Task<IEnumerable<BookDto>> GetAll(int index, int count, string searchText)
        {
            var request = new RequestBuilder()
                .BaseUri(this.Url)
                .AddQuery("index", index)
                .AddQuery("count", count);

            if (!string.IsNullOrEmpty(searchText))
            {
                request.AddQuery("searchText", searchText);
            }
            
            var response = await this.ExecuteRequest(request);
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PaginatedEnumerable<BookDto>>(json);
        }

        public async Task<BookDto> Get(int id)
        {
            var request = new RequestBuilder()
                .BaseUri(this.CombineUrl("{id}"))
                .AddUriSegment("id", id);

            var response = await this.ExecuteRequest(request);
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<BookDto>(json);
        }
    }
}