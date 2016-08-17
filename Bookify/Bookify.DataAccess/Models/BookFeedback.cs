using Bookify.Common.Models;

namespace Bookify.DataAccess.Models
{
    public class BookFeedback
    {
        public int BookId { get; set; }
        public int PersonId { get; set; }
        public int Rating { get; set; }
        public string Text { get; set; }

        public BookFeedbackDto ToDto()
        {
            return new BookFeedbackDto
            {
                BookId = BookId,
                PersonId = PersonId,
                PersonName = "Unknown",
                Rating = Rating,
                Text = Text
            };
        }
    }
}
