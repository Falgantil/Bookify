﻿
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
            var book = await this.Context.Books.Where(b => b.Id == id).Include(b => b.Genres).Include(b => b.Author).Include(x => x.Publisher).SingleAsync();
            return book.ToDto();
        }

        public async Task<IPaginatedEnumerable<BookDto>> GetByFilter(BookFilter filter)
        {
            var search = filter.SearchText?.ToLower();
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
                            b.Author.Name.ToLower().Contains(search) ||
                            b.Publisher.Name.ToLower().Contains(search) ||
                            b.Title.ToLower().Contains(search) ||
                            b.ISBN.ToLower().Contains(search));
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
                Price = command.Price,
            });
            return book.ToDetailedDto();
        }

        public Task<DetailedBookDto> EditBook(UpdateBookCommand command)
        {
            throw new NotImplementedException();
        }
    }
}