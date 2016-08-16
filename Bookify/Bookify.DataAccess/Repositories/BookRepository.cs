
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bookify.Models;
using System.Linq;
using System.Data.Entity;
using Bookify.Core.Extensions;
using Bookify.Core.Interfaces;

namespace Bookify.DataAccess.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(BookifyContext ctx) : base(ctx)
        {

        }

        public override async Task<Book> Find(int id)
        {
            return await _ctx.Books.Where(b => b.Id == id).Include(b => b.Genres).SingleAsync();
        }

        public async Task<IQueryable<Book>> GetAllByParams(int? skip, int? take, int[] genres, string search, string orderBy, bool? desc)
        {
            IQueryable<Book> queryableBooks = await GetAll();

            if (!string.IsNullOrEmpty(search))
                queryableBooks = queryableBooks
                    .Where(b =>
                            string.Equals(b.Author.Name, search, StringComparison.CurrentCultureIgnoreCase) ||
                            b.ISBN == search ||
                            string.Equals(b.Publisher.Name, search, StringComparison.CurrentCultureIgnoreCase) ||
                            string.Equals(b.Title, search, StringComparison.CurrentCultureIgnoreCase));
            // string.Equals (a,b, StringComparison.CurrentCultureIgnoreCase) mean a compare a & b and ignore the case of the string 

            if (genres != null && genres.Any())
            {
                var hashBooks = new HashSet<Book>();
                foreach (var book in queryableBooks)
                {
                    var genresAsIds = book.Genres.Select(x => x.Id);
                    if (genresAsIds.Intersect(genres).Any())
                    {
                        hashBooks.Add(book);
                    }
                }
                queryableBooks = hashBooks.AsQueryable();
            }
            */

            if (!string.IsNullOrEmpty(search))
                booksQuery = booksQuery
                    .Where(b =>
                            string.Equals(b.Author.Name, search, StringComparison.CurrentCultureIgnoreCase) || 
                            b.ISBN == search ||
                            string.Equals(b.Publisher.Name, search, StringComparison.CurrentCultureIgnoreCase) || 
                            string.Equals(b.Title, search, StringComparison.CurrentCultureIgnoreCase));
            // string.Equals (a,b, StringComparison.CurrentCultureIgnoreCase) mean a compare a & b and ignore the case of the string

            if (orderBy != null)
                queryableBooks = queryableBooks.OrderBy(orderBy, desc);

            if (skip != null)
                queryableBooks = queryableBooks.Skip(skip.Value);
            if (take != null)
                queryableBooks = queryableBooks.Take(take.Value);

            return queryableBooks;
        }

        public async Task<Book> FindWithContent(int id)
        {
            return await _ctx.Books.Where(b => b.Id == id).Include(b => b.Genres).Include(b => b.Content).SingleAsync();
        }
    }
}