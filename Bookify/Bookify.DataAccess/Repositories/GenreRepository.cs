using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

using Bookify.Common.Commands.Auth;
using Bookify.Common.Filter;
using Bookify.Common.Models;
using Bookify.Common.Repositories;
using Bookify.DataAccess.Models;

namespace Bookify.DataAccess.Repositories
{
    public class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        public GenreRepository(BookifyContext context) : base(context)
        {

        }

        public async Task<IEnumerable<GenreDto>> GetByFilter(GenreFilter filter)
        {
            var searchText = (filter.Search ?? string.Empty).ToLower();

            var query = await this.GetAll();
            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(g => g.Name.ToLower().Contains(searchText));
            }

            query = query.OrderBy(g => g.Id);

            //var totalCount = query.Count();
            //query = query.Skip(filter.Skip);
            //query = query.Take(filter.Take);
            var collection = await query.ToListAsync();
            return collection.Select(g => g.ToDto()).ToList();
        }

        public async Task<GenreDto> CreateGenre(CreateGenreCommand command)
        {
            var genre = await this.Add(new Genre { Name = command.Name });
            return genre.ToDto();
        }

        public async Task<GenreDto> EditGenre(int id, EditGenreCommand command)
        {
            var genre = await this.Find(id);
            genre.Name = command.Name;
            genre = await this.Update(genre);
            return genre.ToDto();
        }

        public async Task<GenreDto> GetById(int id)
        {
            var genre = await this.Find(id);
            return genre.ToDto();
        }
    }
}
