using Bookify.Common.Models;

namespace Bookify.DataAccess.Models
{
    public class BookFeedback
    {
        public int BookId { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public int Rating { get; set; }
        public string Text { get; set; }
        public Book Book { get; set; }

        public BookFeedbackDto ToDto()
        {
            return new BookFeedbackDto
            {
                BookId = BookId,
                PersonId = PersonId,
                Person = Person.ToDto(),
                Rating = Rating,
                Text = Text
            };
        }
    }
}
