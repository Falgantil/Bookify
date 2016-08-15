using System;

namespace Bookify.Models
{
    public class BookHistory
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public string Attribute { get; set; }
        public string PreviousValue { get; set; }
        public string NewValue { get; set; }
        public BookHistoryType Type { get; set; }
        public Book Book { get; set; }
    }
}