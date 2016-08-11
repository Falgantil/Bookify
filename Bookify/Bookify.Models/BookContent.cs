namespace Bookify.Models
{
    public class BookContent
    {
        public int BookId { get; set; }
        public byte[] Cover { get; set; }
        public byte[] Epub { get; set; }

        public Book Book { get; set; }
    }
}