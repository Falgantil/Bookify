using System;
using Bookify.Core;
using Bookify.Models;

namespace Bookify.DataAccess
{
    internal class BookRepository : GenericRepository<Book>, IBookRepository
    {
        internal BookRepository(BookifyContext ctx) : base(ctx)
        {

        }

        /// <summary>
        /// Adds an entry with the type "Deleted" in bookhistory related to the book
        /// </summary>
        /// <param name="id">Id of the book</param>
        public void Disable(int id)
        {
            var bookHistory = new BookHistory()
            {
                BookId = id,
                Type = BookHistoryType.Deleted,
                Created = DateTime.Now
            };
            _ctx.BookHistory.Add(bookHistory);
            _ctx.SaveChangesAsync();
        }
    }
}