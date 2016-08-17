using System;

using Bookify.Common.Enums;

namespace Bookify.Common.Models
{
    public class BookOrderDto : BaseDto
    {
        public int PersonId { get; set; }
        public BookOrderStatus Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime? ReturnDatetime { get; set; }
    }
}