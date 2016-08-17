using System.Linq;

namespace Bookify.Models.ViewModels
{
    public class BookStatistics
    {
        public Book Book { get; set; }

        public int AmountBought
        {
            get { return Book.Orders.Count(x => x.Status == BookOrderStatus.Sold); }
        }

        public BookHistoryType BookHistory
        {
            get { return Book.History.OrderBy(x => x.Created).FirstOrDefault().Type; }
        }
    }
}
