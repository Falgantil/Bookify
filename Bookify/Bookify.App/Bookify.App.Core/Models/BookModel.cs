namespace Bookify.App.Core.Models
{
    public class BookModel : BaseDataModel
    {
        public string Title { get; set; }

        public int Chapters { get; set; }

        public string Summary { get; set; }

        public string DownloadUrl { get; set; }

        public string CoverUrl { get; set; }

        public string Author { get; set; }

        public bool OwnsBook { get; set; }

        public bool Borrowable { get; set; }
    }

    public class CartItemModel : BaseModel
    {
        public BookModel Book { get; set; }

        public int Quantity { get; set; }
    }
}