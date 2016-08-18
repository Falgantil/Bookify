using System.Threading.Tasks;

using Bookify.Common.Commands.Auth;
using Bookify.Common.Models;
using Bookify.Common.Repositories;
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

        public async Task<BookFeedbackDto> CreateFeedback(int bookid, CreateFeedbackCommand command)
        {
            var bookFeedback = new BookFeedback
            {
                BookId = bookid
            };
            var feedback = await this.Add(bookFeedback);
            return feedback.ToDto();
        }
    }
}
