using System.Threading.Tasks;
using Bookify.Common.Commands.Auth;
using Bookify.Common.Filter;
using Bookify.Common.Models;

namespace Bookify.Common.Repositories
{
    public interface IBookFeedbackRepository
    {
        Task<BookFeedbackDto> CreateFeedback(int bookid, int personId, CreateFeedbackCommand command);
        Task<IPaginatedEnumerable<BookFeedbackDto>> GetByFilter(FeedbackFilter filter);
        Task<BookFeedbackDto> EditFeedback(int bookId, int personId, EditFeedbackCommand command);
        Task DeleteFeedback(int bookId, int personId);
    }
}
