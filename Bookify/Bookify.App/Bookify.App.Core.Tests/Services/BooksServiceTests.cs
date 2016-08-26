using System.Threading.Tasks;
using Bookify.App.Core.Services;
using Bookify.App.Sdk.Interfaces;
using Bookify.Common.Filter;
using Moq;
using Xunit;

namespace Bookify.App.Core.Tests.Services
{
    public class BooksServiceTests
    {
        /// <summary>
        /// Verifies getting books by filter calls the right API method depending on filter.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task VerifyGettingBooksByFilterCallsTheRightApiMethodDependingOnFilter()
        {
            var booksApi = new Mock<IBooksApi>();
            var filesApi = new Mock<IFilesApi>();
            var service = new BooksService(booksApi.Object, filesApi.Object);

            booksApi.Verify(api => api.GetBooks(It.IsAny<BookFilter>()), Times.Never);
            booksApi.Verify(api => api.GetMyBooks(It.IsAny<BookFilter>()), Times.Never);

            await service.GetItems(new BookFilter { MyBooks = false });

            booksApi.Verify(api => api.GetBooks(It.IsAny<BookFilter>()), Times.Once);
            booksApi.Verify(api => api.GetMyBooks(It.IsAny<BookFilter>()), Times.Never);

            await service.GetItems(new BookFilter { MyBooks = true });

            booksApi.Verify(api => api.GetBooks(It.IsAny<BookFilter>()), Times.Once);
            booksApi.Verify(api => api.GetMyBooks(It.IsAny<BookFilter>()), Times.Once);
        }
    }
}