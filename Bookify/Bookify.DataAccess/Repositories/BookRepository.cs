
using System.Threading.Tasks;
using Bookify.Core;
using Bookify.Models;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

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

        public async Task<IQueryable<Book>> GetAllByParams(int? skip, int? take, List<int> genres, string search, string orderBy, bool? desc)
        {
            IQueryable<Book> result = await GetAll();

            if (skip != null)
                result = result.Skip(skip.Value);
            if (take != null)
                result = result.Take(take.Value);
            
            /*
            if (genres != null && genres.Any())
            {
                result = result.Where(b => b.Genres.SelectMany(m => m.Id == genres.First()));
            }
            */

            if (string.IsNullOrEmpty(search))
                result = result.Where(b => b.Author.Name.ToLower() == search || b.ISBN == search || b.Publisher.Name.ToLower() == search || b.Title.ToLower() == search);


            if (orderBy != null)
                result = Core.Extensions.Extensions.OrderBy(result, orderBy, desc);


            return result;
        }

      
        
    }
}