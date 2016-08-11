using System.Collections.Generic;

namespace Bookify.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Book> Books { get; set; } = new HashSet<Book>();
    }
}
