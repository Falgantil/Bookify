using Bookify.Models;

namespace Bookify.App.Core.Models
{
    public class CartItemModel : BaseModel
    {
        public Book Book { get; set; }

        public int Quantity { get; set; }
    }
}