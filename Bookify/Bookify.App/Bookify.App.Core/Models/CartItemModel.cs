using Bookify.Common.Models;

namespace Bookify.App.Core.Models
{
    public class CartItemModel : BaseModel
    {
        public BookDto Book { get; set; }

        public int Quantity { get; set; }
    }
}