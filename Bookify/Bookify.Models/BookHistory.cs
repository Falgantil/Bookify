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
        public BookHistoryType Type { get; set; }
        public Book Book { get; set; }
        public string NewValue
        {
            // Look at Attribute property and Type property, to determine the NewValue to return from the Book object.
            get
            {
                if (Book != null && Type == BookHistoryType.Changed)
                {
                    return Book.GetType().GetProperty(Attribute).GetValue(Book, null).ToString();
                }
                return null;
            }
        }
    }

    public enum BookHistoryType
    {
        Added, Deleted, Approved, Changed
    }
}