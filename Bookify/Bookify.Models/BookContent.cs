namespace Bookify.Models
{
    public class BookContent
    {
        public int BookId { get; set; }
        public string Blob { get; set; }

        public Book Book { get; set; }
    }
}