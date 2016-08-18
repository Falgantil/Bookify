using System.Collections.ObjectModel;
using Bookify.App.Core.Interfaces.Services;
using Bookify.App.Core.Models;

namespace Bookify.App.Core.ViewModels
{
    public class ShoppingBasketViewModel : BaseViewModel
    {
        private readonly IShoppingCartService shoppingCartService;

        public ShoppingBasketViewModel(IShoppingCartService shoppingCartService)
        {
            this.shoppingCartService = shoppingCartService;
        }

        public ObservableCollection<CartItemModel> CartItems => this.shoppingCartService.CartItems;

        public async void RemoveOne(CartItemModel cartItem)
        {
            await this.shoppingCartService.RemoveFromBasket(cartItem.Book);
        }
    }
}