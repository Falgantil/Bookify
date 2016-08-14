namespace Bookify.App.Core.Models
{
    public class BookModel : BaseModel
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
}