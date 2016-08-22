namespace Bookify.Common.Models
{
    public class FeedbackDto : BaseDto
    {
        public int BookId { get; set; }

        public string Message { get; set; }

        public int PersonId { get; set; }

        public string PersonName { get; set; }

        public int Rating { get; set; }
    }
}