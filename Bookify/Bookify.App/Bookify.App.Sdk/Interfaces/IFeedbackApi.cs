using System.Threading.Tasks;
using Bookify.Common.Commands.Auth;
using Bookify.Common.Filter;
using Bookify.Common.Models;

namespace Bookify.App.Sdk.Interfaces
{
    public interface IFeedbackApi
    {
        /// <summary>
        /// Gets the feedback, filtered by <see cref="filter"/>.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        Task<IPaginatedEnumerable<BookFeedbackDto>> GetFeedback(FeedbackFilter filter);

        /// <summary>
        /// Creates the feedback.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        Task<BookFeedbackDto> CreateFeedback(int bookId, CreateFeedbackCommand command);
    }
}