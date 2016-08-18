using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Bookify.API.Controllers;
using Bookify.Common.Filter;
using Bookify.Common.Models;
using Bookify.DataAccess.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;



namespace Bookify.API.Test
{
    [TestClass]
    public class BookController
    {
        //[TestMethod]
        //public async Task GetBook()
        //{
        //    #region Init Mock

        //    var bookRepository = new Mock<IBookRepository>();
        //    bookRepository.Setup(repository => repository.GetByFilter(It.IsAny<BookFilter>())).Returns(
        //        async () =>
        //        {
        //            return
        //                new PaginatedEnumerable<BookDto>(
        //                    new List<BookDto> { new BookDto(), new BookDto(), new BookDto() },
        //                    3,
        //                    0,
        //                    3);
        //        });

        //    var bookHistoryRepository = new Mock<IBookHistoryRepository>();
        //    var personRepository = new Mock<IPersonRepository>();
        //    var bookOrderRepository = new Mock<IBookOrderRepository>();
        //    var bookContentRepository = new Mock<IBookContentRepository>();
        //    var bookFeedbackRepository = new Mock<IBookFeedbackRepository>();
        //    var fileServerRepository = new Mock<IFileServerRepository>();

        //    #endregion

        //    var booksController = new BooksController(
        //        bookRepository.Object,
        //        bookHistoryRepository.Object,
        //        personRepository.Object,
        //        bookOrderRepository.Object,
        //        bookContentRepository.Object,
        //        bookFeedbackRepository.Object,
        //        fileServerRepository.Object);

        //    await GetBookTestValidEmptyFilter(booksController);

        //    #region Change mock

        //    bookRepository.Setup(
        //        repository =>
        //            repository.GetByFilter(It.IsAny<BookFilter>()))
        //        .Returns(async () => null);

        //    #endregion

        //    await GetBookTestValidEmptyFilter1(booksController);
        //}

        public async Task GetBookTestValidEmptyFilter(BooksController booksController)
        {
            var result = await booksController.Get(new BookFilter());
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<List<Book>>));
        }

        public async Task GetBookTestValidEmptyFilter1(BooksController booksController)
        {
            var result = await booksController.Get(new BookFilter());
            Assert.IsInstanceOfType(result, typeof(ExceptionResult));
        }

        [TestMethod]
        public void GetBook(int id)
        {
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void BuyBook()
        {
        }
    }
}
