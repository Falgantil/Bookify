using System.Collections.Generic;

namespace Bookify.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public List<Book> Books { get; set; }
    }
}