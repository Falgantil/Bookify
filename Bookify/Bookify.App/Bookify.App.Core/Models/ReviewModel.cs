using System;

namespace Bookify.App.Core.Models
{
    public class ReviewModel : BaseModel
    {
        public string Author { get; set; }

        public DateTime CreatedTs { get; set; }

        public string Message { get; set; }

        public int Rating { get; set; }
    }
}