using System.Collections.Generic;

namespace Bookify.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public List<Genre> Genres { get; set; }
    }
}
