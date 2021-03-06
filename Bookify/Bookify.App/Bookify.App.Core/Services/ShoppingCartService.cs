using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using Bookify.App.Core.Interfaces.Services;
using Bookify.App.Core.Models;
using Bookify.Common.Models;

namespace Bookify.App.Core.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        /// <summary>
        /// The caching region
        /// </summary>
        private readonly ICachingRegion<IEnumerable<CartItemModel>> cachingRegion;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShoppingCartService"/> class.
        /// </summary>
        /// <param name="cachingRegionFactory">The caching region factory.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public ShoppingCartService(ICachingRegionFactory cachingRegionFactory)
        {
            if (cachingRegionFactory == null)
                throw new ArgumentNullException(nameof(cachingRegionFactory));
            this.cachingRegion = cachingRegionFactory.CreateRegion<IEnumerable<CartItemModel>>("ShoppingCart");

            this.LoadItems();
        }

        /// <summary>
        /// Loads the items from the caching region.
        /// </summary>
        private async void LoadItems()
        {
            var items = await this.cachingRegion.GetItem();
            if (items == null)
            {
                return;
            }
            foreach (var i in items)
            {
                this.CartItems.Add(i);
            }
        }

        /// <summary>
        /// Gets the books that currently reside in the cart.
        /// </summary>
        /// <value>
        /// The books.
        /// </value>
        public ObservableCollection<CartItemModel> CartItems { get; } = new ObservableCollection<CartItemModel>();

        /// <summary>
        /// Adds the specified book to the cart.
        /// </summary>
        /// <param name="book">The book.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task AddToCart(BookDto book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));
            var oldItem = this.CartItems.FirstOrDefault(model => model.Book.Id == book.Id);
            if (oldItem != null)
            {
                oldItem.Quantity++;
                await this.cachingRegion.UpdateItem(this.CartItems);
                return;
            }
            this.CartItems.Add(new CartItemModel
            {
                Quantity = 1,
                Book = book
            });
            await this.cachingRegion.UpdateItem(this.CartItems);
        }

        /// <summary>
        /// Removes the book from the cart.
        /// </summary>
        /// <param name="book">The book.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// The specified Book was not found as an item in the shopping cart!
        /// </exception>
        public async Task RemoveFromCart(BookDto book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            var oldItem = this.CartItems.FirstOrDefault(m => m.Book == book);
            if (oldItem == null)
            {
                throw new ArgumentNullException(nameof(book), "The specified Book was not found as an item in the shopping cart!");
            }
            this.CartItems.Remove(oldItem);
            await this.cachingRegion.UpdateItem(this.CartItems);
        }

        /// <summary>
        /// Removes the book with the specified ID from the cart.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">A cart item with a book with the specified ID was not found in the shopping cart!</exception>
        public async Task RemoveFromCart(int bookId)
        {
            var oldItem = this.CartItems.FirstOrDefault(m => m.Book.Id == bookId);
            if (oldItem == null)
            {
                throw new ArgumentNullException(nameof(bookId), "A cart item with a book with the specified ID was not found in the shopping cart!");
            }
            this.CartItems.Remove(oldItem);
            await this.cachingRegion.UpdateItem(this.CartItems);
        }
    }
}