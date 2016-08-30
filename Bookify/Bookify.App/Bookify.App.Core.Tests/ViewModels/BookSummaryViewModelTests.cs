using Bookify.App.Core.Collections;
using Bookify.App.Core.Interfaces.Services;
using Bookify.App.Core.ViewModels;
using Bookify.Common.Filter;
using Bookify.Common.Models;
using Moq;
using Shouldly;
using Xunit;

namespace Bookify.App.Core.Tests.ViewModels
{
    public class BookSummaryViewModelTests
    {
        [Fact]
        public void VerifyOwnsBookChangesWhenBookIsAddedToMyBooksCollection()
        {
            var book = new DetailedBookDto
            {
                Id = 5
            };

            var booksService = new Mock<IBooksService>();
            var collection = new ObservableServiceCollection<BookDto, BookFilter, IBooksService>(booksService.Object);
            booksService.SetupGet(s => s.MyBooks).Returns(collection);
            var shoppingCartService = new Mock<IShoppingCartService>();
            var authService = new Mock<IAuthenticationService>();
            var viewModel = new BookSummaryViewModel(book, booksService.Object, shoppingCartService.Object, authService.Object);

            int ownsBookChanged = 0;

            viewModel.OwnsBook.ShouldBeFalse();
            viewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName != nameof(viewModel.OwnsBook))
                {
                    return;
                }
                ownsBookChanged++;
            };
            collection.Add(book);

            viewModel.OwnsBook.ShouldBeTrue();
            ownsBookChanged.ShouldBe(1);
        }
    }
}