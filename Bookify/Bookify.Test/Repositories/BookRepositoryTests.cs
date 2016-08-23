using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Bookify.DataAccess.Models;
using Bookify.Common.Enums;
using System.Threading.Tasks;
using Bookify.DataAccess;
using Moq;
using System.Data.Entity;
using Bookify.DataAccess.Repositories;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Bookify.Common.Filter;
using Bookify.Common.Repositories;

namespace Bookify.Test.Repositories
{
    [TestClass]
    public class BookRepositoryTests
    {
        //public IQueryable<Book> _books;

        //[TestMethod]
        //public async Task FindById()
        //{
        //    // Arrange
        //    ArrangeStaticBooks();
        //    var mockSet = new Mock<DbSet<Book>>();
        //    mockSet.As<IDbAsyncEnumerable<Book>>()
        //        .Setup(m => m.GetAsyncEnumerator())
        //        .Returns(new TestDbAsyncEnumerator<Book>(_books.GetEnumerator()));

        //    mockSet.As<IQueryable<Book>>()
        //        .Setup(m => m.Provider)
        //        .Returns(new TestDbAsyncQueryProvider<Book>(_books.Provider));

        //    mockSet.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(_books.Expression);
        //    mockSet.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(_books.ElementType);
        //    mockSet.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(_books.GetEnumerator());

        //    var mockContext = new Mock<BookifyContext>();
        //    mockContext.Setup(x => x.Books).Returns(mockSet.Object);

        //    var bookRepo = new BookRepository(mockContext.Object);
            
        //    // Act
        //    var books = await bookRepo.GetByFilter(new BookFilter());

        //    // Assert
        //    Assert.AreEqual(_books.Count(), books.Count());
        //}

        //private void ArrangeStaticBooks()
        //{
        //    var genres = new[] {
        //        new Genre {Id = 1, Name = "Biografi"},
        //        new Genre {Id = 2, Name = "Eventyr"},
        //        new Genre {Id = 3, Name = "Krimi"},
        //        new Genre {Id = 4, Name = "Science fiction"},
        //        new Genre {Id = 5, Name = "Børn"},
        //        new Genre {Id = 6, Name = "Humor"},
        //        new Genre {Id = 7, Name = "Spænding"}
        //    };

        //    var authors = new[]
        //    {
        //        new Author { Id = 1, Name = "Jesper F. Jensen" },
        //        new Author { Id = 2, Name = "Maria Lang" }
        //    };

        //    var publishers = new[]
        //    {
        //        new Publisher { Id = 1, Name = "DigTea", Trusted = true },
        //        new Publisher { Id = 2, Name = "Rosenkilde & Bahnhof", Trusted = false }
        //    };

        //    List<Book> books = new List<Book>() {
        //        new Book
        //        {
        //            Id = 1,
        //            ISBN = "9788771744422",
        //            Author = authors[0],
        //            Publisher = publishers[0],
        //            Title = "Farlig at drikke",
        //            Price = 250,
        //            Summary = "Krimi fra et stockholmsk gymnasium, hvor skolekomediens primadonna og en kvindelig journalist bliver myrdet ved forårsballet. Christer Wijk mistænker en af skolens lærere for mordene",
        //            Language = "Dansk",
        //            PublishYear = 2015,
        //            ViewCount = 0,
        //            Genres = { genres[0] },
        //            CopiesAvailable = 3,
        //            History = new List<BookHistory>()
        //            {
        //                new BookHistory()
        //                {
        //                    Type = BookHistoryType.Added,
        //                    Created = DateTime.Now
        //                },
        //                new BookHistory()
        //                {
        //                    Type = BookHistoryType.Approved,
        //                    Created = DateTime.Now
        //                },
        //                new BookHistory()
        //                {
        //                    Type = BookHistoryType.Changed,
        //                    Attribute = "Price",
        //                    PreviousValue = "100",
        //                    NewValue = "200",
        //                    Created = DateTime.Now
        //                },new BookHistory()
        //                {
        //                    Type = BookHistoryType.Changed,
        //                    Attribute = "Price",
        //                    PreviousValue = "200",
        //                    NewValue = "250",
        //                    Created = DateTime.Now
        //                }
        //            }
        //        },
        //        new Book
        //        {
        //            Id = 2,
        //            ISBN = "9788771744477",
        //            Author = authors[0],
        //            Publisher = publishers[1],
        //            Title = "Kun en skygge",
        //            Price = 200,
        //            Summary = "En ung kvinde vender hjem fra en udenlandsrejse og finder, i stedet for sin mand, en myrdet kvinde i sit badekar. Kriminalkommissær Christer Wijk får en vanskelig opgave med at trænge ind i det specielle akademikermiljø præget af misundelse og kærlighedsrelationer på kryds og tværs",
        //            Language = "Dansk",
        //            PublishYear = 2014,
        //            ViewCount = 0,
        //            Genres = { genres[2], genres[5], genres[6] },
        //            CopiesAvailable = 20,
        //            History = new List<BookHistory>()
        //            {
        //                new BookHistory()
        //                {
        //                    Type = BookHistoryType.Added,
        //                    Created = DateTime.Now
        //                },
        //                new BookHistory()
        //                {
        //                    Type = BookHistoryType.Approved,
        //                    Created = DateTime.Now
        //                },
        //                new BookHistory()
        //                {
        //                    Type = BookHistoryType.Changed,
        //                    Attribute = "Title",
        //                    PreviousValue = "woah my first book!",
        //                    NewValue = "Kun en skygge",
        //                    Created = DateTime.Now
        //                },new BookHistory()
        //                {
        //                    Type = BookHistoryType.Changed,
        //                    Attribute = "Price",
        //                    PreviousValue = "3.50",
        //                    NewValue = "200",
        //                    Created = DateTime.Now
        //                }
        //            }
        //        },
        //        new Book
        //        {
        //            Id = 3,
        //            ISBN = "9788793183360",
        //            Author = authors[1],
        //            Publisher = publishers[0],
        //            Title = "Little Red Ridinghood",
        //            Price = 150,
        //            Summary = "Rødhætte er en lille artig pige. En dag sender mor hende ud til bedstemors hus. Men da hun møder hr. Ulv, glemmer hun, hvad mor har sagt",
        //            Language = "Engelsk",
        //            PublishYear = 2014,
        //            ViewCount = 0,
        //            Genres = { genres[1], genres[3], genres[4] },
        //            CopiesAvailable = 4,
        //            History = new List<BookHistory>()
        //            {
        //                new BookHistory()
        //                {
        //                    Type = BookHistoryType.Added,
        //                    Created = DateTime.Now
        //                },
        //                new BookHistory()
        //                {
        //                    Type = BookHistoryType.Approved,
        //                    Created = DateTime.Now
        //                },
        //                new BookHistory()
        //                {
        //                    Type = BookHistoryType.Changed,
        //                    Attribute = "PublishYear",
        //                    PreviousValue = "0001",
        //                    NewValue = "2014",
        //                    Created = DateTime.Now
        //                },new BookHistory()
        //                {
        //                    Type = BookHistoryType.Deleted,
        //                    Created = DateTime.Now
        //                }
        //            }
        //        },
        //        new Book
        //        {
        //            Id = 4,
        //            ISBN = "9788771690194",
        //            Author = authors[1],
        //            Publisher = publishers[1],
        //            Title = "Thor and the ugly giant",
        //            Price = 200,
        //            Summary = "Der er kommet en jætte ind i Odins borg. Kan Thor få jætten jaget ud ved hjælp af list?",
        //            Language = "Engelsk",
        //            PublishYear = 2013,
        //            ViewCount = 0,
        //            Genres = { genres[2], genres[6], genres[3] },
        //            CopiesAvailable = 4,
        //            History = new List<BookHistory>()
        //            {
        //                new BookHistory()
        //                {
        //                    Type = BookHistoryType.Added,
        //                    Created = DateTime.Now
        //                },
        //                new BookHistory()
        //                {
        //                    Type = BookHistoryType.Approved,
        //                    Created = DateTime.Now
        //                }
        //            }
        //        }
        //    };
        //    _books = books.AsQueryable();
        //}
    }
}
