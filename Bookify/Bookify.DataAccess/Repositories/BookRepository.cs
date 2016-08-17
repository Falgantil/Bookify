
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

using Bookify.Common.Commands.Auth;
using Bookify.Common.Filter;
using Bookify.Common.Models;
using Bookify.Core.Extensions;
using Bookify.DataAccess.Interfaces.Repositories;
using Bookify.DataAccess.Models;
using Bookify.DataAccess.Models.ViewModels;

namespace Bookify.DataAccess.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(BookifyContext context) : base(context)
        {

        }

        public async Task<BookDto> GetById(int id)
        {
            var book = await this.Context.Books.Where(b => b.Id == id).Include(b => b.Genres).Include(b => b.Author).Include(x => x.Publisher).SingleAsync();
            return book.ToDto();
        }

        public async Task<IPaginatedEnumerable<BookDto>> GetByFilter(BookFilter filter)
        {
            var search = filter.SearchText;
            var genres = filter.GenreIds;
            var orderBy = filter.OrderBy;
            var desc = filter.Descending;
            var index = filter.Index;
            var count = filter.Count;

            var queryableBooks = await this.GetAll();

            if (!string.IsNullOrEmpty(search))
            {
                queryableBooks =
                    queryableBooks.Where(
                        b =>
                        string.Equals(b.Author.Name, search, StringComparison.CurrentCultureIgnoreCase)
                        || b.ISBN == search
                        || string.Equals(b.Publisher.Name, search, StringComparison.CurrentCultureIgnoreCase)
                        || string.Equals(b.Title, search, StringComparison.CurrentCultureIgnoreCase));
            }

            if (genres != null && genres.Any())
            {
                foreach (var genreId in genres)
                {
                    var genreId1 = genreId;
                    queryableBooks = queryableBooks.Where(book => book.Genres.Any(k => k.Id == genreId1));
                }
            }

            queryableBooks = queryableBooks.Include(x => x.Genres);
            queryableBooks = queryableBooks.OrderBy(orderBy, desc);

            var totalCount = queryableBooks.Count();

            queryableBooks = queryableBooks.Skip(index);
            queryableBooks = queryableBooks.Take(count);

            var collection = await queryableBooks.ToListAsync();
            foreach (var g in collection.SelectMany(b => b.Genres))
            {
                g.Books = new Book[0];
            }

            return new PaginatedEnumerable<BookDto>(collection.Select(b => b.ToDto()), totalCount, index, count);
        }

        public async Task<BookStatisticsDto> FindForStatistics(int id)
        {
            var book = await this.Context.Books.Where(b => b.Id == id).Include(b => b.History).Include(b => b.Orders).Include(b => b.Feedback).SingleAsync();
            var detailedBookDto = book.ToDetailedDto();
            return new BookStatisticsDto
            {
                Book = detailedBookDto
            };
        }
        
        public Task<DetailedBookDto> CreateBook(CreateBookCommand command)
        {
            throw new NotImplementedException();
        }

        public Task<DetailedBookDto> EditBook(UpdateBookCommand command)
        {
            throw new NotImplementedException();
        }
    }
}