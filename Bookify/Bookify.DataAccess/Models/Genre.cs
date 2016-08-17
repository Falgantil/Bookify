using System.Collections.Generic;

using Bookify.Common.Models;

namespace Bookify.DataAccess.Models
{
    public class Genre
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Book> Books { get; set; } = new HashSet<Book>();

        public GenreDto ToDto()
        {
            return new GenreDto { Id = Id, Name = Name };
        }
    }
}
