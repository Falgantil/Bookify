using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Bookify.API.Controllers;
using Bookify.Core;
using Bookify.Core.Filter;
using Bookify.Core.Interfaces;
using Bookify.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Bookify.UnitTets
{
    [TestClass]
    public class BookController
    {
        [TestMethod]
        public async Task GetBook()
        {
            #region Init Mock

            var bookRepository = new Mock<IBookRepository>();
            bookRepository.Setup(
                repository =>
                    repository.GetAllByParams(It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<int[]>(), It.IsAny<string>(),
                        It.IsAny<string>(), It.IsAny<bool?>()))
                .Returns(async () =>
                {
                    return new EnumerableQuery<Book>(new List<Book>
                    {
                        new Book(),
                        new Book(),
                        new Book()
                    });
                });

            var bookHistoryRepository = new Mock<IBookHistoryRepository>();
            var personRepository = new Mock<IPersonRepository>();
            var bookOrderRepository = new Mock<IBookOrderRepository>();
            var bookContentRepository = new Mock<IBookContentRepository>();
            var bookFeedbackRepository = new Mock<IBookFeedbackRepository>();

            #endregion

            var booksController = new BooksController(
                bookRepository.Object,
                bookHistoryRepository.Object,
                personRepository.Object,
                bookOrderRepository.Object,
                bookContentRepository.Object,
                bookFeedbackRepository.Object);

            await GetBookTestValidEmptyFilter(booksController);

            #region Change mock

            bookRepository.Setup(
                repository =>
                    repository.GetAllByParams(It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<int[]>(), It.IsAny<string>(),
                        It.IsAny<string>(), It.IsAny<bool?>()))
                .Returns(async () => null);

            #endregion

            await GetBookTestValidEmptyFilter1(booksController);
        }

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