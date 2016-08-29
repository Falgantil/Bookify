using System.Threading.Tasks;

using Bookify.App.Core.Interfaces.Services;
using Bookify.App.Sdk.Interfaces;
using Bookify.Common.Commands.Auth;
using Bookify.Common.Filter;
using Bookify.Common.Models;

namespace Bookify.App.Core.Services
{
    /// <summary>
    /// The Feedback Service implementation.
    /// </summary>
    /// <seealso cref="IFeedbackService" />
    public class FeedbackService : IFeedbackService
    {
        /// <summary>
        /// The API
        /// </summary>
        private readonly IFeedbackApi api;

        /// <summary>
        /// Initializes a new instance of the <see cref="FeedbackService"/> class.
        /// </summary>
        /// <param name="api">The API.</param>
        public FeedbackService(IFeedbackApi api)
        {
            this.api = api;
        }

        /// <summary>
        /// Gets the items, using the provided Filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public async Task<IPaginatedEnumerable<BookFeedbackDto>> GetItems(FeedbackFilter filter)
        {
            return await this.api.GetFeedback(filter);
        }

        /// <summary>
        /// Creates the feedback.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <param name="rating">The rating.</param>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public async Task<BookFeedbackDto> CreateFeedback(int bookId, int rating, string message)
        {
            return await this.api.CreateFeedback(bookId, new CreateFeedbackCommand
            {
                Text = message,
                Rating = rating
            });
        }
    }
}