namespace Bookify.Models
{
    public class BookFeedback
    {
        public int BookId { get; set; }
        public int PersonId { get; set; }
        public int Rating { get; set; }
        public string Text { get; set; }
    }
}
