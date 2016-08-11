using System.Collections.Generic;

namespace Bookify.Models
{
    public class Publisher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Trusted { get; set; }

        public ICollection<Book> Books { get; set; } = new HashSet<Book>();
        public ICollection<Person> Persons { get; set; } = new HashSet<Person>();
    }
}