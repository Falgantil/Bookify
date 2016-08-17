using System.Threading.Tasks;

using Bookify.Common.Commands.Auth;
using Bookify.DataAccess.Models;

namespace Bookify.DataAccess.Interfaces.Repositories
{
    public interface IBookFeedbackRepository : IGenericRepository<BookFeedback>
    {
        Task<BookFeedback> CreateFeedback(int bookid, CreateFeedbackCommand command);
    }
}
