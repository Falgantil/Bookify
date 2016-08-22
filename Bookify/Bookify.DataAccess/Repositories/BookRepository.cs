
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;
using Bookify.Common.Commands.Auth;
using Bookify.Common.Exceptions;
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

        public async Task<DetailedBookDto> GetById(int id)
        {
            var book =
                await
                    this.Context.Books.Where(b => b.Id == id)
                        .Include(b => b.Genres)
                        .Include(b => b.Author)
                        .Include(x => x.Publisher)
                        .Include(f => f.Feedback)
                        .SingleAsync();
            return book.ToDetailedDto();
        }

        public async Task<IPaginatedEnumerable<BookDto>> GetByFilter(BookFilter filter)
        {
            var search = filter.Search?.ToLower();
            var author = filter.Author;
            var genres = filter.Genres;
            var orderBy = filter.OrderBy;
            var desc = filter.Descending;
            var index = filter.Skip;
            var count = filter.Take;

            var queryableBooks = await this.GetAll();

            if (!string.IsNullOrEmpty(search))
            {
                queryableBooks =
                    queryableBooks.Where(
                        b =>
                            b.Author.Name.ToLower().Contains(search) ||
                            b.Publisher.Name.ToLower().Contains(search) ||
                            b.Title.ToLower().Contains(search) ||
                            b.ISBN.ToLower().Contains(search));
            }

            if (author.HasValue)
            {
                queryableBooks = queryableBooks.Where(x => x.AuthorId == author.Value);
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

            return new PaginatedEnumerable<BookDto>(collection.Select(b => b.ToDto()), totalCount);
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
            List<Genre> genres = new List<Genre>();
            foreach (var genreId in command.Genres)
            {
                genres.Add(new Genre { Id = genreId });
            }
            //IQueryable<Genre> genres = this.Context.Genres.AsQueryable();
            //foreach (var genre in command.Genres)
            //{
            //    var genre1 = genre;
            //    genres = genres.Where(x => x.Id == genre1);
            //}
            //var dbGenres = await genres.ToListAsync();

            var book = await this.Add(new Book
            {
                Title = command.Title,
                Summary = command.Summary,
                AuthorId = command.AuthorId,
                PublishYear = command.PublishYear,
                Genres = genres, //dbGenres.Select(genre => genre).ToList(),
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

        public async Task<DetailedBookDto> EditBook(int id, UpdateBookCommand command)
        {
            var book = await this.Find(id);
            if (book == null) throw  new NotFoundException($"the requested item with id: {id} could not be found");
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
            book.Title = string.IsNullOrEmpty(command.Title)? command.Title : book.Title;
            book.Summary = string.IsNullOrEmpty(command.Summary)? command.Summary : book.Summary;
            book.AuthorId = command.AuthorId > 0 ? command.AuthorId.Value : book.AuthorId;
            book.PublishYear = command.PublishYear ?? book.PublishYear;
            if (dbGenres.Any())
            {
                book.Genres = dbGenres.Select(genre => genre).ToList();
            }
            book.PublisherId = command.PublisherId > 0 ? command.PublisherId.Value : book.PublisherId;
            book.Language = string.IsNullOrEmpty(command.Language)? command.Language : book.Language;
            book.CopiesAvailable = command.CopiesAvailable ?? book.CopiesAvailable;
            book.PageCount = command.PageCount ?? book.PageCount;
            book.ViewCount = command.ViewCount ?? book.ViewCount;
            book.ISBN = string.IsNullOrEmpty(command.ISBN)? command.ISBN : book.ISBN;
            book.Price = command.Price ?? book.Price;
            

            //woah
            var resultBook = await this.Update(book);
            return resultBook.ToDetailedDto();
        }
    }
}