using System.Collections.Generic;

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
        public decimal Price { get; set; }
        public int PublishYear { get; set; }
        public int? PageCount { get; set; }
        public int? CopiesAvailable { get; set; }
        public int ViewCount { get; set; }

        public Author Author { get; set; }
        public Publisher Publisher { get; set; }
        public List<BookHistory> History { get; set; }
        public List<Genre> Genres { get; set; }
        public List<BookOrder> Orders { get; set; }
    }
}
