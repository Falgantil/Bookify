using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

using Bookify.Common.Commands.Auth;
using Bookify.Common.Exceptions;
using Bookify.Common.Filter;
using Bookify.Common.Models;
using Bookify.Common.Repositories;
using Bookify.DataAccess.Models;

namespace Bookify.DataAccess.Repositories
{
    public class BookFeedbackRepository : GenericRepository<BookFeedback>, IBookFeedbackRepository
    {
        public BookFeedbackRepository(BookifyContext context) : base(context)
        {
        }

        public async Task<BookFeedbackDto> CreateFeedback(int bookid, CreateFeedbackCommand command)
        {
            var bookFeedback = new BookFeedback
            {
                BookId = bookid,
                Text = command.Text,
                Rating = command.Rating
            };
            var feedback = await this.Add(bookFeedback);
            return feedback.ToDto();
        }

        public async Task<IPaginatedEnumerable<BookFeedbackDto>> GetByFilter(FeedbackFilter filter)
        {
            if (!(filter.BookId > 0)) throw new BadRequestException($"BookId was less than 0, did you forget to supply a bookid?");
            var bookId = filter.BookId;
            var skip = filter.Skip;
            var take = filter.Take;

            var feedbacks = this.Context.BookFeedback.Where(x => x.BookId == bookId).Include(x => x.Person).OrderBy(x => x.BookId).ThenBy(x => x.PersonId);
            var feedbackAmount = feedbacks.Count();


            var feedbacksOrdered = feedbacks.Skip(skip);
            feedbacksOrdered = feedbacksOrdered.Take(take);


            var feedbacksList = await feedbacksOrdered.ToListAsync();

            return new PaginatedEnumerable<BookFeedbackDto>(feedbacksList.Select(x => x.ToDto()), feedbackAmount);
        }

        public async Task<BookFeedbackDto> EditFeedback(int id, UpdateFeedbackCommand command)
        {
            var feedback = await this.Find(id);
            if (feedback == null) throw new NotFoundException($"the requested item with id: {id} could not be found");

            // All units, concentrate all available resources to the target that needs amending...
            feedback.Rating = command.Rating > 0 ? command.Rating.Value : feedback.Rating;
            feedback.Text = string.IsNullOrEmpty(command.Text) ? command.Text : feedback.Text;
            // Mission succesfull
            var updatedFeedback = await this.Update(feedback);
            return updatedFeedback.ToDto();
        }

        public async Task DeleteFeedback(int bookId, int personId)
        {
            var feedback = await this.Context.BookFeedback.Where(x => x.BookId == bookId && x.PersonId == personId).SingleAsync();
            //var fb2 = this.Context.BookFeedback.Find(new { BookId = bookId, PersonId = personId });
            await this.Remove(feedback);

        }
    }
}
