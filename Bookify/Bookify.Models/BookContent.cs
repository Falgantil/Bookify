namespace Bookify.Models
{
    public class BookContent
    {
        public int BookId { get; set; }
        public string CoverPath { get; set; }
        public string EpubPath { get; set; }

        public Book Book { get; set; }
    }
}