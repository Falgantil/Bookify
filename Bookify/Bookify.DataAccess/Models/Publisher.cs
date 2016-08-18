using System.Collections.Generic;

using Bookify.Common.Models;

using Newtonsoft.Json;

namespace Bookify.DataAccess.Models
{
    public class Publisher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Trusted { get; set; }

        public ICollection<Book> Books { get; set; } = new HashSet<Book>();
        public ICollection<Person> Persons { get; set; } = new HashSet<Person>();

        public PublisherDto ToDto()
        {
            return new PublisherDto
            {
                Id = Id,
                Name = Name,
                Trusted = Trusted
            };
        }
    }
}