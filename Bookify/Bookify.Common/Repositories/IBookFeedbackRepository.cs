using System.Threading.Tasks;
using Bookify.Common.Commands.Auth;
using Bookify.Common.Filter;
using Bookify.Common.Models;

namespace Bookify.Common.Repositories
{
    public interface IBookFeedbackRepository
    {
        Task<BookFeedbackDto> CreateFeedback(int bookid, CreateFeedbackCommand command);
        Task<IPaginatedEnumerable<BookFeedbackDto>> GetByFilter(FeedbackFilter filter);
        Task<BookFeedbackDto> EditFeedback(int id, UpdateFeedbackCommand command);
        Task DeleteFeedback(int id);
    }
}
