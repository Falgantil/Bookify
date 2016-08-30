using Bookify.API.Controllers;
using Bookify.Common.Commands.Auth;
using Bookify.Common.Enums;
using Bookify.Common.Filter;
using Bookify.Common.Models;
using Bookify.Common.Repositories;
using Bookify.DataAccess.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace Bookify.Test.APIControllers
{
    [TestClass]
    public class BooksControllerTests
    {
        [TestMethod]
        public async Task Get_SingleBookById()
        {
            // Arrange
            var bookRepo = new Mock<IBookRepository>();
            var bookHistoryRepo = new Mock<IBookHistoryRepository>();
            //var bookFeedbackRepo = new Mock<IBookFeedbackRepository>();
            var authRepo = new Mock<IAuthenticationRepository>();
            var personRepository = new Mock<IPersonRepository>();
            var bookOrderRepository = new Mock<IBookOrderRepository>();

            bookRepo.Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(async (int id) => { return GetStaticBooks().Where(x => x.Id == id).Single().ToDetailedDto(); });

            var controller = new BooksController(bookRepo.Object, bookHistoryRepo.Object, authRepo.Object, personRepository.Object, bookOrderRepository.Object);

            // Act
            IHttpActionResult actionResult = await controller.Get(1);
            var contentResult = actionResult as OkNegotiatedContentResult<DetailedBookDto>;

            // Assert
            DetailedBookDto expectedBookDto = GetStaticBooks().First().ToDetailedDto();
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(expectedBookDto.Id, contentResult.Content.Id);
            Assert.AreEqual(JsonConvert.SerializeObject(expectedBookDto), JsonConvert.SerializeObject(contentResult.Content));
        }

        [TestMethod]
        public async Task Get_SingleBookReturnsNullOnNotFound()
        {
            // Arrange
            var bookRepo = new Mock<IBookRepository>();
            var bookHistoryRepo = new Mock<IBookHistoryRepository>();
            var bookFeedbackRepo = new Mock<IBookFeedbackRepository>();
            var authRepo = new Mock<IAuthenticationRepository>();
            var personRepository = new Mock<IPersonRepository>();
            var bookOrderRepository = new Mock<IBookOrderRepository>();

            bookRepo.Setup(x => x.GetById(100))
                .Returns(async () =>
                {
                    return GetStaticBooks().Where(x => x.Id == 100).SingleOrDefault()?.ToDetailedDto();
                });

            var controller = new BooksController(bookRepo.Object, bookHistoryRepo.Object, authRepo.Object, personRepository.Object, bookOrderRepository.Object);

            // Act
            IHttpActionResult actionResult = await controller.Get(100);
            var contentResult = actionResult as OkNegotiatedContentResult<DetailedBookDto>;

            // Assert
            Assert.IsNull(contentResult.Content);
        }

        [TestMethod]
        public async Task Create_AddsTheBook()
        {
            // Arrange
            var createBookCommand = new CreateBookCommand
            {
                Title = "Heste bog",
                Summary = "Noget tekst",
                ISBN = "1234567890"
            };

            var bookRepo = new Mock<IBookRepository>();
            var bookHistoryRepo = new Mock<IBookHistoryRepository>();
            var bookFeedbackRepo = new Mock<IBookFeedbackRepository>();
            var authRepo = new Mock<IAuthenticationRepository>();
            var personRepository = new Mock<IPersonRepository>();
            var bookOrderRepository = new Mock<IBookOrderRepository>();

            bookRepo.Setup(x => x.CreateBook(createBookCommand))
                .Returns(async () =>
                {
                    return new DetailedBookDto
                    {
                        Id = 5,
                        Title = createBookCommand.Title,
                        ISBN = createBookCommand.ISBN
                    };
                });

            var controller = new BooksController(bookRepo.Object, bookHistoryRepo.Object, authRepo.Object, personRepository.Object, bookOrderRepository.Object);

            // Act
            var actionResult = await controller.Create(createBookCommand);
            var contentResult = actionResult as OkNegotiatedContentResult<DetailedBookDto>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(5, contentResult.Content.Id);
        }

        [TestMethod]
        public async Task Update_UpdatesBook()
        {
            // Arrange
            var bookRepo = new Mock<IBookRepository>();
            var bookHistoryRepo = new Mock<IBookHistoryRepository>();
            var bookFeedbackRepo = new Mock<IBookFeedbackRepository>();
            var authRepo = new Mock<IAuthenticationRepository>();
            var personRepository = new Mock<IPersonRepository>();
            var bookOrderRepository = new Mock<IBookOrderRepository>();

            bookRepo.Setup(x => x.EditBook(It.IsAny<int>(), It.IsAny<EditBookCommand>()))
                .Returns(async (int id, EditBookCommand cmd) =>
                {
                    var bookToUpdate = GetStaticBooks().Where(x => x.Id == id).Single();
                    bookToUpdate.Price = (decimal)cmd.Price;
                    return bookToUpdate.ToDetailedDto();
                });

            var controller = new BooksController(bookRepo.Object, bookHistoryRepo.Object, authRepo.Object, personRepository.Object, bookOrderRepository.Object);

            // Act
            var actionResult = await controller.Update(1, new EditBookCommand { Price = 1337 });
            var contentResult = actionResult as OkNegotiatedContentResult<DetailedBookDto>;

            // Assert
            Assert.AreEqual(1, contentResult.Content.Id);
            Assert.AreEqual(1337, contentResult.Content.Price);
        }

        #region Static Data
        private IEnumerable<Book> GetStaticBooks()
        {
            var genres = new[] {
                new Genre {Id = 1, Name = "Biografi"},
                new Genre {Id = 2, Name = "Eventyr"},
                new Genre {Id = 3, Name = "Krimi"},
                new Genre {Id = 4, Name = "Science fiction"},
                new Genre {Id = 5, Name = "Børn"},
                new Genre {Id = 6, Name = "Humor"},
                new Genre {Id = 7, Name = "Spænding"}
            };

            var authors = new[]
            {
                new Author { Id = 1, Name = "Jesper F. Jensen" },
                new Author { Id = 2, Name = "Maria Lang" }
            };

            var books = new List<Book>() {
                new Book
                {
                    Id = 1,
                    ISBN = "9788771744422",
                    Author = authors[0],
                    PublisherId = 2,
                    Title = "Farlig at drikke",
                    Price = 250,
                    Summary = "Krimi fra et stockholmsk gymnasium, hvor skolekomediens primadonna og en kvindelig journalist bliver myrdet ved forårsballet. Christer Wijk mistænker en af skolens lærere for mordene",
                    Language = "Dansk",
                    PublishYear = 2015,
                    ViewCount = 0,
                    Genres = { genres[0] },
                    CopiesAvailable = 3,
                    History = new List<BookHistory>()
                    {
                        new BookHistory()
                        {
                            Type = BookHistoryType.Added,
                            Created = DateTime.Now
                        },
                        new BookHistory()
                        {
                            Type = BookHistoryType.Approved,
                            Created = DateTime.Now
                        },
                        new BookHistory()
                        {
                            Type = BookHistoryType.Changed,
                            Attribute = "Price",
                            PreviousValue = "100",
                            NewValue = "200",
                            Created = DateTime.Now
                        },new BookHistory()
                        {
                            Type = BookHistoryType.Changed,
                            Attribute = "Price",
                            PreviousValue = "200",
                            NewValue = "250",
                            Created = DateTime.Now
                        }
                    }
                },
                new Book
                {
                    Id = 2,
                    ISBN = "9788771744477",
                    Author = authors[0],
                    PublisherId = 2,
                    Title = "Kun en skygge",
                    Price = 200,
                    Summary = "En ung kvinde vender hjem fra en udenlandsrejse og finder, i stedet for sin mand, en myrdet kvinde i sit badekar. Kriminalkommissær Christer Wijk får en vanskelig opgave med at trænge ind i det specielle akademikermiljø præget af misundelse og kærlighedsrelationer på kryds og tværs",
                    Language = "Dansk",
                    PublishYear = 2014,
                    ViewCount = 0,
                    Genres = { genres[2], genres[5], genres[6] },
                    CopiesAvailable = 20,
                    History = new List<BookHistory>()
                    {
                        new BookHistory()
                        {
                            Type = BookHistoryType.Added,
                            Created = DateTime.Now
                        },
                        new BookHistory()
                        {
                            Type = BookHistoryType.Approved,
                            Created = DateTime.Now
                        },
                        new BookHistory()
                        {
                            Type = BookHistoryType.Changed,
                            Attribute = "Title",
                            PreviousValue = "woah my first book!",
                            NewValue = "Kun en skygge",
                            Created = DateTime.Now
                        },new BookHistory()
                        {
                            Type = BookHistoryType.Changed,
                            Attribute = "Price",
                            PreviousValue = "3.50",
                            NewValue = "200",
                            Created = DateTime.Now
                        }
                    }
                },
                new Book
                {
                    Id = 3,
                    ISBN = "9788793183360",
                    Author = authors[1],
                    PublisherId = 1,
                    Title = "Little Red Ridinghood",
                    Price = 150,
                    Summary = "Rødhætte er en lille artig pige. En dag sender mor hende ud til bedstemors hus. Men da hun møder hr. Ulv, glemmer hun, hvad mor har sagt",
                    Language = "Engelsk",
                    PublishYear = 2014,
                    ViewCount = 0,
                    Genres = { genres[1], genres[3], genres[4] },
                    CopiesAvailable = 4,
                    History = new List<BookHistory>()
                    {
                        new BookHistory()
                        {
                            Type = BookHistoryType.Added,
                            Created = DateTime.Now
                        },
                        new BookHistory()
                        {
                            Type = BookHistoryType.Approved,
                            Created = DateTime.Now
                        },
                        new BookHistory()
                        {
                            Type = BookHistoryType.Changed,
                            Attribute = "PublishYear",
                            PreviousValue = "0001",
                            NewValue = "2014",
                            Created = DateTime.Now
                        },new BookHistory()
                        {
                            Type = BookHistoryType.Deleted,
                            Created = DateTime.Now
                        }
                    }
                },
                new Book
                {
                    Id = 4,
                    ISBN = "9788771690194",
                    Author = authors[1],
                    PublisherId = 1,
                    Title = "Thor and the ugly giant",
                    Price = 200,
                    Summary = "Der er kommet en jætte ind i Odins borg. Kan Thor få jætten jaget ud ved hjælp af list?",
                    Language = "Engelsk",
                    PublishYear = 2013,
                    ViewCount = 0,
                    Genres = { genres[2], genres[6], genres[3] },
                    CopiesAvailable = 4,
                    History = new List<BookHistory>()
                    {
                        new BookHistory()
                        {
                            Type = BookHistoryType.Added,
                            Created = DateTime.Now
                        },
                        new BookHistory()
                        {
                            Type = BookHistoryType.Approved,
                            Created = DateTime.Now
                        }
                    }
                }
            };
            return books;
        }
        #endregion
    }
}
