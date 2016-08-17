using System.Collections.Generic;
using System.Linq;

namespace Bookify.Models
{
    public class Book
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public int PublisherId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string ISBN { get; set; }
        public string Language { get; set; }
        public decimal Price { get; set; }
        public int PublishYear { get; set; }
        public int? PageCount { get; set; }
        public int? CopiesAvailable { get; set; }
        public int ViewCount { get; set; }

        public double AverageRating { get { return Feedback.Count > 0 ? Feedback.Average(x => x.Rating) : 0; } }

        public Author Author { get; set; }
        public Publisher Publisher { get; set; }
        //public BookContent Content { get; set; }
        public ICollection<BookHistory> History { get; set; } = new HashSet<BookHistory>();
        public ICollection<Genre> Genres { get; set; } = new HashSet<Genre>();
        public ICollection<BookOrder> Orders { get; set; } = new HashSet<BookOrder>();
        public ICollection<BookFeedback> Feedback { get; set; } = new HashSet<BookFeedback>();
    }
}
