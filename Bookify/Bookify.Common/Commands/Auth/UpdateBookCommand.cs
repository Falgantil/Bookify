using System.Collections.Generic;

namespace Bookify.Common.Commands.Auth
{
    public class UpdateBookCommand
    {
        public string ISBN;
        public int? BookId { get; set; }
        public IEnumerable<int> Genres { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public int? AuthorId { get; set; }
        public int? PublishYear { get; set; }
        public int? PublisherId { get; set; }
        public string Language { get; set; }
        public int? CopiesAvailable { get; set; }
        public int? PageCount { get; set; }
        public decimal? Price { get; set; }
        public int? ViewCount { get; set; }
    }
}