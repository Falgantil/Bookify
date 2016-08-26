using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Bookify.App.Core.Interfaces.Services;
using Bookify.App.Core.Models;

namespace Bookify.App.Core.ViewModels
{
    public class ShoppingCartViewModel : BaseViewModel
    {
        private readonly IShoppingCartService shoppingCartService;

        public ShoppingCartViewModel(IShoppingCartService shoppingCartService)
        {
            this.shoppingCartService = shoppingCartService;
        }

        public ObservableCollection<CartItemModel> CartItems => this.shoppingCartService.CartItems;

        public async Task RemoveOne(CartItemModel cartItem)
        {
            await this.shoppingCartService.RemoveFromCart(cartItem.Book);
        }
    }
}