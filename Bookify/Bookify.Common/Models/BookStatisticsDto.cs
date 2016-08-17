using System.Linq;

using Bookify.Common.Enums;

namespace Bookify.Common.Models
{
    public class BookStatisticsDto
    {
        public DetailedBookDto Book { get; set; }

        public int AmountBought
        {
            get { return this.Book.Orders.Count(x => x.Status == BookOrderStatus.Sold); }
        }

        public BookHistoryType BookHistory
        {
            get { return this.Book.History.OrderBy(x => x.Created).FirstOrDefault().Type; }
        }
    }
}