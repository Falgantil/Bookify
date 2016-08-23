using Bookify.Common.Filter;
using Bookify.DataAccess;
using Bookify.DataAccess.Models;
using Bookify.DataAccess.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Bookify.Test.Repositories
{
    [TestClass]
    public class GenreRepositoryTests
    {
        [TestMethod]
        public async Task GetAllGenres()
        {
            //var data = new List<Genre>
            //{
            //    new Genre {Id = 1, Name = "Biografi"},
            //    new Genre {Id = 2, Name = "Eventyr"},
            //    new Genre {Id = 3, Name = "Krimi"},
            //    new Genre {Id = 4, Name = "Science fiction"},
            //    new Genre {Id = 5, Name = "Børn"},
            //    new Genre {Id = 6, Name = "Humor"},
            //    new Genre {Id = 7, Name = "Spænding"}
            //}.AsQueryable();

            //var mockSet = new Mock<DbSet<Genre>>();
            //mockSet.As<IQueryable<Genre>>().Setup(x => x.Provider).Returns(data.Provider);
            //mockSet.As<IQueryable<Genre>>().Setup(x => x.Expression).Returns(data.Expression);
            //mockSet.As<IQueryable<Genre>>().Setup(x => x.ElementType).Returns(data.ElementType);
            //mockSet.As<IQueryable<Genre>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            //var mockContext = new Mock<BookifyContext>();
            //mockContext.Setup(c => c.Genres).Returns(mockSet.Object);

            //var service = new GenreRepository(mockContext.Object);
            //var genres = await service.GetByFilter(new GenreFilter());

            //Assert.IsTrue(true);


            /*
            var mockSet = new Mock<DbSet<Genre>>();

            var mockContext = new Mock<BookifyContext>();
            mockContext.Setup(x => x.Genres).Returns(mockSet.Object);

            var service = new GenreRepository(mockContext.Object);

            await service.CreateGenre(new Common.Commands.Auth.CreateGenreCommand { Name = "Hest" });
            mockContext.Verify(x => x.SaveChanges(), Times.Once);
            */
        }
    }
}
