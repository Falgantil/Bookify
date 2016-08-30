using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Bookify.App.Core.Interfaces.Services;
using Bookify.App.Core.Models;

namespace Bookify.App.Core.ViewModels
{
    public class ShoppingCartViewModel : BaseViewModel
    {
        /// <summary>
        /// The shopping cart service
        /// </summary>
        private readonly IShoppingCartService shoppingCartService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShoppingCartViewModel" /> class.
        /// </summary>
        /// <param name="shoppingCartService">The shopping cart service.</param>
        public ShoppingCartViewModel(IShoppingCartService shoppingCartService)
        {
            this.shoppingCartService = shoppingCartService;
        }

        /// <summary>
        /// Gets the cart items.
        /// </summary>
        /// <value>
        /// The cart items.
        /// </value>
        public ObservableCollection<CartItemModel> CartItems => this.shoppingCartService.CartItems;

        /// <summary>
        /// Removes one entity from the specified <see cref="cartItem" />.
        /// </summary>
        /// <param name="cartItem">The cart item.</param>
        /// <returns></returns>
        public async Task RemoveOne(CartItemModel cartItem)
        {
            await this.shoppingCartService.RemoveFromCart(cartItem.Book);
        }
    }
}