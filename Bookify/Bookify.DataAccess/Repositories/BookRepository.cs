
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;
using Bookify.Common.Commands.Auth;
using Bookify.Common.Filter;
using Bookify.Common.Models;
using Bookify.Common.Repositories;
using Bookify.DataAccess.Extensions;
using Bookify.DataAccess.Models;

namespace Bookify.DataAccess.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(BookifyContext context) : base(context)
        {

        }

        public async Task<BookDto> GetById(int id)
        {
            var book =
                await
                    this.Context.Books.Where(b => b.Id == id)
                        .Include(b => b.Genres)
                        .Include(b => b.Author)
                        .Include(x => x.Publisher)
                        .SingleAsync();
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
                            b.Author.Name.StartsWith(search) || b.Author.Name.EndsWith(search) ||
                            b.Publisher.Name.StartsWith(search) || b.Publisher.Name.EndsWith(search) ||
                            b.Title.StartsWith(search) || b.Title.EndsWith(search) ||
                            b.ISBN.StartsWith(search) || b.ISBN.EndsWith(search));
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
            var book =
                await
                    this.Context.Books.Where(b => b.Id == id)
                        .Include(b => b.History)
                        .Include(b => b.Orders)
                        .Include(b => b.Feedback)
                        .SingleAsync();
            var detailedBookDto = book.ToDetailedDto();
            return new BookStatisticsDto
            {
                Book = detailedBookDto
            };
        }

        public async Task<DetailedBookDto> CreateBook(CreateBookCommand command)
        {
            IQueryable<Genre> genres = this.Context.Genres.AsQueryable();
            foreach (var genre in command.Genres)
            {
                var genre1 = genre;
                genres = genres.Where(x => x.Id == genre1);
            }
            var dbGenres = await genres.ToListAsync();

            var book = await this.Add(new Book
            {
                Title = command.Title,
                Summary = command.Summary,
                AuthorId = command.AuthorId,
                PublishYear = command.PublishYear,
                Genres = dbGenres.Select(genre => genre).ToList(),
                PublisherId = command.PublisherId,
                Language = command.Language,
                CopiesAvailable = command.CopiesAvailable,
                PageCount = command.PageCount,
                ViewCount = 0,
                ISBN = command.ISBN,
                Price = command.Price
            });
            return book.ToDetailedDto();
        }

        public async Task<DetailedBookDto> EditBook(UpdateBookCommand command)
        {
            if (!command.BookId.HasValue) return null;
            var book = await this.Find(command.BookId.Value);
            List<Genre> dbGenres = new List<Genre>();
            if (command.Genres.Any())
            {
                IQueryable<Genre> genres = this.Context.Genres.AsQueryable();
                foreach (var genre in command.Genres)
                {
                    var genre1 = genre;
                    genres = genres.Where(x => x.Id == genre1);
                }
                dbGenres = await genres.ToListAsync();
            }

            // Commence updating book...
            book.Title = command.Title ?? book.Title;
            book.Summary = command.Summary ?? book.Title;
            book.AuthorId = command.AuthorId ?? book.AuthorId;
            book.PublishYear = command.PublishYear ?? book.PublishYear;
            if (dbGenres.Any())
            {
                book.Genres = dbGenres.Select(genre => genre).ToList();
            }
            book.PublisherId = command.PublisherId ?? book.PublisherId;
            book.Language = command.Language ?? book.Language;
            book.CopiesAvailable = command.CopiesAvailable ?? book.CopiesAvailable;
            book.PageCount = command.PageCount ?? book.PageCount;
            book.ViewCount = command.ViewCount ?? book.ViewCount;
            book.ISBN = command.ISBN ?? book.ISBN;
            book.Price = command.Price ?? book.Price;
            

            //woah
            var resultBook = await this.Update(book);
            return resultBook.ToDetailedDto();
        }
    }
}