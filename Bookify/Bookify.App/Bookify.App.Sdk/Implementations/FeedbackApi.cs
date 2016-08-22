using System;
using System.Threading.Tasks;
using Bookify.App.Sdk.Interfaces;
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

        public async Task<IPaginatedEnumerable<FeedbackDto>> GetFeedback(FeedbackFilter filter)
        {
            if (filter.BookId == 0)
            {
                throw new ArgumentException("Missing Book ID!", nameof(filter.BookId));
            }

            var request = new RequestBuilder()
                .BaseUri(UrlHelper.Combine(this.Url, "{bookid}"))
                .AddUriSegment("{bookid}", filter.BookId)
                .AddQuery(nameof(filter.Skip), filter.Skip)
                .AddQuery(nameof(filter.Take), filter.Take);
            if (!string.IsNullOrEmpty(filter.Search))
            {
                request.AddQuery(nameof(filter.Search), filter.Search);
            }
            return await this.ExecuteAndParse<PaginatedEnumerable<FeedbackDto>>(request);
        }
    }
}