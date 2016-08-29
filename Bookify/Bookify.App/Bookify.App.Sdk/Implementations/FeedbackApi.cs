using System;
using System.Net.Http;
using System.Threading.Tasks;
using Bookify.App.Sdk.Interfaces;
using Bookify.Common.Commands.Auth;
using Bookify.Common.Filter;
using Bookify.Common.Models;

namespace Bookify.App.Sdk.Implementations
{
    public class FeedbackApi : BaseApi, IFeedbackApi
    {
        public FeedbackApi() 
            : base(ApiConfig.FeedbackRoot)
        {
        }

        public async Task<IPaginatedEnumerable<BookFeedbackDto>> GetFeedback(FeedbackFilter filter)
        {
            if (filter.BookId == 0)
            {
                throw new ArgumentException("Missing Book ID!", nameof(filter.BookId));
            }

            var request = new RequestBuilder()
                .BaseUri(this.Url)
                .AddQuery(nameof(filter.BookId), filter.BookId)
                .AddQuery(nameof(filter.Skip), filter.Skip)
                .AddQuery(nameof(filter.Take), filter.Take);
            if (!string.IsNullOrEmpty(filter.Search))
            {
                request.AddQuery(nameof(filter.Search), filter.Search);
            }

            return await this.ExecuteAndParse<PaginatedEnumerable<BookFeedbackDto>>(request);
        }

        public async Task<BookFeedbackDto> CreateFeedback(int bookId, CreateFeedbackCommand command)
        {
            if (bookId == 0)
            {
                throw new ArgumentException("Missing Book ID!", nameof(bookId));
            }

            var request = new RequestBuilder()
                .BaseUri(this.CombineUrl("{id}"))
                .AddUriSegment("id", bookId)
                .JsonContent(command)
                .Method(HttpMethod.Post);

            return await this.ExecuteAndParse<BookFeedbackDto>(request);
        }
    }
}