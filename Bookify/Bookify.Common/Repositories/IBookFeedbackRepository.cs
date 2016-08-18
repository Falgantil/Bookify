using System.Threading.Tasks;
using Bookify.Common.Commands.Auth;
using Bookify.Common.Models;

namespace Bookify.Common.Repositories
{
    public interface IBookFeedbackRepository
    {
        Task<BookFeedbackDto> CreateFeedback(int bookid, CreateFeedbackCommand command);
    }
}
