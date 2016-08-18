using System.Collections.Generic;

using Bookify.Common.Models;

using Newtonsoft.Json;

namespace Bookify.DataAccess.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Book> Books { get; set; } = new HashSet<Book>();

        public AuthorDto ToDto()
        {
            return new AuthorDto
            {
                Id = Id,
                Name = Name
            };
        }
    }
}