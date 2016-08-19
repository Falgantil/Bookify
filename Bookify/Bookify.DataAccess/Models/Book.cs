using System.Collections.Generic;
using System.Linq;

using Bookify.Common.Models;

namespace Bookify.DataAccess.Models
{
    public class Book : BaseModel
    {
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

        public double AverageRating { get { return this.Feedback.Count > 0 ? this.Feedback.Average(x => x.Rating) : 0; } }

        public Author Author { get; set; }
        public Publisher Publisher { get; set; }
        //public BookContent Content { get; set; }
        public ICollection<BookHistory> History { get; set; } = new HashSet<BookHistory>();
        public ICollection<Genre> Genres { get; set; } = new HashSet<Genre>();
        public ICollection<BookOrder> Orders { get; set; } = new HashSet<BookOrder>();
        public ICollection<BookFeedback> Feedback { get; set; } = new HashSet<BookFeedback>();

        public BookDto ToDto()
        {
            return new BookDto
            {
                Id = Id,
                Publisher = Publisher?.ToDto(),
                Author = Author?.ToDto(),
                Genres = Genres.Select(x => x.ToDto()).ToList(),
                Price = Price,
                PublishYear = PublishYear,
                Summary = Summary,
                Title = Title
            };
        }

        public DetailedBookDto ToDetailedDto()
        {
            return new DetailedBookDto
            {
                Id = Id,
                Publisher = Publisher?.ToDto(),
                Author = Author?.ToDto(),
                Genres = Genres.Select(x => x.ToDto()).ToList(),
                Price = Price,
                PublishYear = PublishYear,
                Summary = Summary,
                Title = Title,
                Feedback = Feedback.Select(f => f.ToDto()).ToList()
            };
        }
    }
}
