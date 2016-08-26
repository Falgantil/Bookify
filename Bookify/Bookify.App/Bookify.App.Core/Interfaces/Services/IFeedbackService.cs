using System.Threading.Tasks;
using Bookify.Common.Filter;
using Bookify.Common.Models;

namespace Bookify.App.Core.Interfaces.Services
{
    /// <summary>
    /// The Feedback Service interface. 
    /// Inherits from <see cref="IGetByFilterService{TDto,TFilter}"/>, 
    /// and contains extra methods relevant for the Feedback Service.
    /// </summary>
    /// <seealso cref="Services.IGetByFilterService{BookFeedbackDto, FeedbackFilter}" />
    public interface IFeedbackService : IGetByFilterService<BookFeedbackDto, FeedbackFilter>
    {
        /// <summary>
        /// Creates the feedback.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <param name="rating">The rating.</param>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        Task<BookFeedbackDto> CreateFeedback(int bookId, int rating, string message);
    }
}