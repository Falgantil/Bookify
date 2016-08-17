using System;

using Bookify.Common.Enums;

namespace Bookify.Common.Models
{
    public class BookHistoryDto : BaseDto
    {
        public int BookId { get; set; }

        public string Attribute { get; set; }

        public string PreviousValue { get; set; }

        public string NewValue { get; set; }

        public BookHistoryType Type { get; set; }

        public DateTime Created { get; set; }
    }
}