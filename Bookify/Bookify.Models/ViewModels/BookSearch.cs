using System.Collections.Generic;

namespace Bookify.Models.ViewModels
{
    public class BookSearch
    {
        public IEnumerable<Book> Books { get; set; }
        public int BookCount { get; set; }
    }
}
