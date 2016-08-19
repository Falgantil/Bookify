// Bookify.App
// - Bookify.App.Core.Tests
// -- ShoppingBasketViewModelTests.cs
// -------------------------------------------
// Author: Bjarke Søgaard <sogaardbjarke@gmail.com>

using System.Threading.Tasks;

using Bookify.App.Core.Interfaces.Services;
using Bookify.App.Core.Models;
using Bookify.App.Core.ViewModels;
using Bookify.Common.Models;

using Moq;

using Xunit;

namespace Bookify.App.Core.Tests.ViewModels
{
    public class ShoppingCartViewModelTests
    {
        [Fact]
        public async Task VerifyRemoveItemGetsCalledOnServiceWhenRemovingItem()
        {
            var shoppingCartService = new Mock<IShoppingCartService>();
            var viewModel = new ShoppingCartViewModel(shoppingCartService.Object);

            var book = new BookDto { Id = 5, Title = "Some book" };
            var cartItem = new CartItemModel
            {
                Book = book
            };
            await viewModel.RemoveOne(cartItem);

            shoppingCartService.Verify(s => s.RemoveFromCart(book), Times.Once);
        }
    }
}