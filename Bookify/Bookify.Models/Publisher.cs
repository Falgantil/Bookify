using System.Collections.Generic;

namespace Bookify.Models
{
    public class Publisher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Trusted { get; set; }

        public List<Book> Books { get; set; }
        public List<Person> Persons { get; set; }
    }
}