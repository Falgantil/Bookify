using Bookify.Common.Models;

namespace Bookify.App.Core.Models
{
    public class CartItemModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the book.
        /// </summary>
        /// <value>
        /// The book.
        /// </value>
        public BookDto Book { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>
        /// The quantity.
        /// </value>
        public int Quantity { get; set; }
    }
}