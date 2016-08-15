using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Bookify.App.Core.Models;

namespace Bookify.App.Core.Interfaces.Services
{
    /// <summary>
    /// The <see cref="IBasketService"/> interface.
    /// </summary>
    public interface IBasketService
    {
        /// <summary>
        /// Gets the books that currently reside in the basket.
        /// </summary>
        /// <value>
        /// The books.
        /// </value>
        ObservableCollection<BookModel> Books { get; }

        /// <summary>
        /// Adds the specified book to the basket.
        /// </summary>
        /// <param name="book">The book.</param>
        /// <returns></returns>
        Task AddToBasket(BookModel book);
    }
}