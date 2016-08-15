using Bookify.Core;
using Bookify.Models;

namespace Bookify.DataAccess.Repositories
{
    public class BookFeedbackRepository : GenericRepository<BookFeedback>, IBookFeedbackRepository
    {
        public BookFeedbackRepository(BookifyContext ctx) : base(ctx)
        {

        }
    }
}
