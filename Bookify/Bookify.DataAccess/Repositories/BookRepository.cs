
using System;
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
            IQueryable<Book> booksQuery = await GetAll();

                // var res = listA.Where(n => !listB.Contains(n));
            /*
            List<Book> tempResult = new List<Book>();
            if (genres != null && genres.Any())
            {
                //result = result.Where(b => b.Genres..Contains())
                foreach (var book in result)
                {
                    foreach (var bookgenre in book.Genres)
                    {
                        foreach (var genre in genres)
                        {
                            if (bookgenre.Id == genre) {
                                tempResult.Add(book); // go to next book 
                            }
                        }
                    }
                }
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
                booksQuery = booksQuery.OrderBy(orderBy, desc);
            // Order by is a Extension -> doens't need to call the object it lays in to exec the method ;)

            if (skip != null)
                booksQuery = booksQuery.Skip(skip.Value);
            if (take != null)
                booksQuery = booksQuery.Take(take.Value);
            
            return booksQuery;
        }

      
        
    }
}