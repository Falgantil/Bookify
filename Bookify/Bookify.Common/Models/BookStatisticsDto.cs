using System.Linq;

using Bookify.Common.Enums;

namespace Bookify.Common.Models
{
    public class BookStatisticsDto
    {
        /// <summary>
        /// Gets or sets the book.
        /// </summary>
        /// <value>
        /// The book.
        /// </value>
        public DetailedBookDto Book { get; set; }

        /// <summary>
        /// Gets the amount bought.
        /// </summary>
        /// <value>
        /// The amount bought.
        /// </value>
        public int AmountBought
        {
            get { return this.Book.Orders.Count(x => x.Status == BookOrderStatus.Sold); }
        }

        /// <summary>
        /// Gets the book history.
        /// </summary>
        /// <value>
        /// The book history.
        /// </value>
        public BookHistoryType BookHistory
        {
            get { return this.Book.History.OrderBy(x => x.Created).FirstOrDefault().Type; }
        }
    }
}