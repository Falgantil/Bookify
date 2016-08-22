using System;

namespace Bookify.App.Core.Models
{
    public class ReviewModel : BaseDataModel
    {
        public string PersonName { get; set; }

        public int PersonId { get; set; }

        public string Message { get; set; }

        public int Rating { get; set; }
    }
}