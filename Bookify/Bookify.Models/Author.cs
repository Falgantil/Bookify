using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bookify.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<Book> Books { get; set; } = new HashSet<Book>();
    }
}