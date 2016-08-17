using System.Threading.Tasks;

using Bookify.Common.Commands.Auth;
using Bookify.DataAccess.Interfaces.Repositories;
using Bookify.DataAccess.Models;

namespace Bookify.DataAccess.Repositories
{
    public class BookFeedbackRepository : GenericRepository<BookFeedback>, IBookFeedbackRepository
    {
        private readonly IAuthenticationRepository authRepository;

        public BookFeedbackRepository(BookifyContext context, IAuthenticationRepository authRepository) : base(context)
        {
            this.authRepository = authRepository;
        }

        public async Task<BookFeedback> CreateFeedback(int bookid, CreateFeedbackCommand command)
        {
            var bookFeedback = new BookFeedback
            {
                BookId = bookid
            };
            return this.Context.BookFeedback.Add(bookFeedback);
        }
    }
}
