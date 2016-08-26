// Bookify.App
// - Bookify.App.Core.Tests
// -- ShoppingCartServiceTests.cs
// -------------------------------------------
// Author: Bjarke Søgaard <sogaardbjarke@gmail.com>

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Bookify.App.Core.Interfaces.Services;
using Bookify.App.Core.Models;
using Bookify.App.Core.Services;
using Bookify.Common.Models;

using Moq;

using Shouldly;

using Xunit;

namespace Bookify.App.Core.Tests.Services
{
    public class ShoppingCartServiceTests
    {
        [Fact]
        public async Task VerifyAddingToCartAddsItToTheCollection()
        {
            var cachingRegionFactory = new Mock<ICachingRegionFactory>();
            var cachingRegion = new Mock<ICachingRegion<IEnumerable<CartItemModel>>>();
            cachingRegionFactory.Setup(f => f.CreateRegion<IEnumerable<CartItemModel>>("ShoppingCart")).Returns(cachingRegion.Object);
            var service = new ShoppingCartService(cachingRegionFactory.Object);

            var book = new BookDto { Id = 2 };
            await service.AddToCart(book);

            service.CartItems.Count.ShouldBe(1);
            service.CartItems[0].Book.ShouldBe(book);
        }

        [Fact]
        public async Task VerifyRemovingFromCartRemovesItFromTheCollection()
        {
            var cachingRegionFactory = new Mock<ICachingRegionFactory>();
            var cachingRegion = new Mock<ICachingRegion<IEnumerable<CartItemModel>>>();
            cachingRegionFactory.Setup(f => f.CreateRegion<IEnumerable<CartItemModel>>("ShoppingCart")).Returns(cachingRegion.Object);
            var service = new ShoppingCartService(cachingRegionFactory.Object);

            var book = new BookDto { Id = 2 };
            await service.AddToCart(book);

            service.CartItems.Count.ShouldBe(1);
            service.CartItems[0].Book.ShouldBe(book);

            await service.RemoveFromCart(book.Id);

            service.CartItems.Count.ShouldBe(0);
        }
    }
}