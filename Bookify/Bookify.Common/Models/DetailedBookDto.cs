using System.Collections.Generic;

namespace Bookify.Common.Models
{
    public class DetailedBookDto : BookDto
    {
        public IEnumerable<BookFeedbackDto> Feedback { get; set; }

        public IEnumerable<BookOrderDto> Orders { get; set; }

        public IEnumerable<BookHistoryDto> History { get; set; }
    }
}