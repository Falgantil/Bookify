using System.Threading.Tasks;
using Bookify.App.Core.Interfaces.Services;
using Bookify.App.Core.ViewModels;
using Bookify.Common.Filter;
using Bookify.Common.Models;
using Moq;
using Shouldly;
using Xunit;

namespace Bookify.App.Core.Tests.ViewModels
{
    public class SearchViewModelTests
    {
        [Fact]
        public void VerifySearchingInvokesService()
        {
            var booksService = new Mock<IBooksService>();
            booksService.Setup(s => s.GetItems(It.IsAny<BookFilter>()))
                .ReturnsAsync(new PaginatedEnumerable<BookDto>(new[]
                {
                    new BookDto(), new BookDto(), new BookDto()
                }, 3));
            var delayService = new Mock<IDelayService>();
            //var source = new TaskCompletionSource<bool>();
            //delayService.Setup(service => service.Delay(It.IsAny<int>())).Returns(() => source.Task);
            delayService.Setup(service => service.Delay(It.IsAny<int>())).Returns(async () => { });
            var viewModel = new SearchViewModel(booksService.Object, delayService.Object);

            booksService.Verify(s => s.GetItems(It.IsAny<BookFilter>()), Times.Never);
            viewModel.SearchText = "Fisk";
            booksService.Verify(s => s.GetItems(It.IsAny<BookFilter>()), Times.Once);
        }

        [Fact]
        public void VerifyConsecutiveEditToSearchTextOnlyInvokesSearchOnce()
        {
            var booksService = new Mock<IBooksService>();
            booksService.Setup(s => s.GetItems(It.IsAny<BookFilter>()))
                .ReturnsAsync(new PaginatedEnumerable<BookDto>(new[]
                {
                    new BookDto(), new BookDto(), new BookDto()
                }, 3));
            var delayService = new Mock<IDelayService>();
            var source = new TaskCompletionSource<bool>();
            delayService.Setup(service => service.Delay(It.IsAny<int>())).Returns(() => source.Task);
            var viewModel = new SearchViewModel(booksService.Object, delayService.Object);

            booksService.Verify(s => s.GetItems(It.IsAny<BookFilter>()), Times.Never);
            for (int i = 0; i < 10; i++)
            {
                viewModel.SearchText = $"Fisk {i}";
            }
            booksService.Verify(s => s.GetItems(It.IsAny<BookFilter>()), Times.Never);
            source.SetResult(true);
            booksService.Verify(s => s.GetItems(It.IsAny<BookFilter>()), Times.Once);
        }

        [Fact]
        public void VerifyWhenGenreIsSetThatItSearchesWithGenreToo()
        {
            var booksService = new Mock<IBooksService>();
            BookFilter filter = null;
            booksService.Setup(s => s.GetItems(It.IsAny<BookFilter>()))
                .Returns(async (BookFilter f) =>
                {
                    filter = f;
                    return new PaginatedEnumerable<BookDto>(new[]
                    {
                        new BookDto(), new BookDto(), new BookDto()
                    }, 3);
                });
            var delayService = new Mock<IDelayService>();
            delayService.Setup(service => service.Delay(It.IsAny<int>())).Returns(async () => { });
            var viewModel = new SearchViewModel(booksService.Object, delayService.Object);
            var genre = new GenreDto { Id = 42 };
            viewModel.Genre = genre;

            viewModel.SearchText = "Fisk";
            filter.ShouldNotBeNull();
            booksService.Verify(service => service.GetItems(filter), Times.Once);
            filter.Genres.Length.ShouldBe(1);
            filter.Genres[0].ShouldBe(42);

        }
    }
}