using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Bookify.App.Core.Annotations;
using Bookify.App.Core.Models;
using Bookify.Models;

namespace Bookify.App.Core.Interfaces.Services
{
    /// <summary>
    /// The <see cref="IShoppingCartService"/> interface.
    /// </summary>
    public interface IShoppingCartService
    {
        /// <summary>
        /// Gets the books that currently reside in the basket.
        /// </summary>
        /// <value>
        /// The books.
        /// </value>
        ObservableCollection<CartItemModel> CartItems { get; }

        /// <summary>
        /// Adds the specified book to the basket.
        /// </summary>
        /// <param name="book">The book.</param>
        /// <returns></returns>
        Task AddToBasket(Book book);

        /// <summary>
        /// Removes the book from the basket.
        /// </summary>
        /// <param name="book">The book.</param>
        /// <returns></returns>
        Task RemoveFromBasket(Book book);

        /// <summary>
        /// Removes the book with the specified ID from the basket.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <returns></returns>
        Task RemoveFromBasket(int bookId);
    }

    public class ShoppingCartService : IShoppingCartService
    {
        private readonly ICachingRegion<IEnumerable<CartItemModel>> cachingRegion;

        public ShoppingCartService([NotNull] ICachingRegionFactory cachingRegionFactory)
        {
            if (cachingRegionFactory == null)
                throw new ArgumentNullException(nameof(cachingRegionFactory));
            this.cachingRegion = cachingRegionFactory.CreateRegion<IEnumerable<CartItemModel>>("ShoppingCart");

            this.LoadItems();
        }

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

        public ObservableCollection<CartItemModel> CartItems { get; } = new ObservableCollection<CartItemModel>();

        public async Task AddToBasket([NotNull] Book book)
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

        public async Task RemoveFromBasket([NotNull] Book book)
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

        public async Task RemoveFromBasket(int bookId)
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