using System;

namespace Bookify.Models
{
    public class BookOrder
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int PersonId { get; set; }
        public BookOrderStatus Status { get; set; }
        public DateTime Datetime { get; set; }
        public DateTime? ReturnDatetime { get; set; }

        public Person Person { get; set; }
        public Book Book { get; set; }
    }

    public enum BookOrderStatus
    {
        Borrowed, Sold, Queued, Dequeued
    }
}