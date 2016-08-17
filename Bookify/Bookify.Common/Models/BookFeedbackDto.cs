namespace Bookify.Common.Models
{
    public class BookFeedbackDto
    {
        public int BookId { get; set; }

        public int PersonId { get; set; }

        public string PersonName { get; set; }

        public int Rating { get; set; }

        public string Text { get; set; }
    }
}