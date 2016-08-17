using System.Threading.Tasks;

using Bookify.DataAccess.Interfaces.Repositories;
using Bookify.DataAccess.Models;

namespace Bookify.DataAccess.Repositories
{
    public class BookContentRepository : GenericRepository<BookContent>, IBookContentRepository
    {
        public BookContentRepository(BookifyContext context) : base(context)
        {

        }

        public async Task<BookContent> GetById(int id)
        {
            return await this.Find(id);
        }
    }
}
