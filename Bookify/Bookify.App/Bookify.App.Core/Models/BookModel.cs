using System;

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
    }

    public class LightBookModel : BaseModel
    {
        public string Title { get; set; }

        public string ThumbnailUrl { get; set; }
    }

    public class ReviewModel : BaseModel
    {
        public string Author { get; set; }

        public DateTime CreatedTs { get; set; }

        public string Message { get; set; }

        public int Rating { get; set; }
    }
}