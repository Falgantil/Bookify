using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Bookify.App.Sdk.Interfaces;
using Bookify.Models;
using Bookify.Models.ViewModels;
using Newtonsoft.Json;

namespace Bookify.App.Sdk.Implementations
{
    public class BookApi : BaseApi, IBookApi
    {
        public BookApi()
            : base(AppConfig.BooksRoot)
        {
        }

        public async Task<IEnumerable<Book>> GetAll(int index, int count, string searchText)
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
            var deserializeObject = JsonConvert.DeserializeObject<BookSearch>(json);
            return deserializeObject.Books;
        }

        public async Task<Book> Get(int id)
        {
            var request = new RequestBuilder()
                .BaseUri(this.CombineUrl("book/{id}"))
                .AddUriSegment("id", id);

            var response = await this.ExecuteRequest(request);
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Book>(json);
        }
    }
}