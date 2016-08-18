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

        public async Task<IPaginatedEnumerable<GenreDto>> GetByFilter(GenreFilter filter)
        {
            var searchText = (filter.SearchText ?? string.Empty).ToLower();

            var query = await this.GetAll();
            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(g => g.Name.ToLower().Contains(searchText));
            }

            query = query.OrderBy(g => g.Id);

            var totalCount = query.Count();
            query = query.Skip(filter.Index);
            query = query.Take(filter.Count);
            var collection = await query.ToListAsync();
            return new PaginatedEnumerable<GenreDto>(collection.Select(g => g.ToDto()), totalCount, filter.Index, filter.Count);
        }

        public async Task<GenreDto> CreateGenre(CreateGenreCommand command)
        {
            var genre = await this.Add(new Genre { Name = command.Name });
            return genre.ToDto();
        }

        public async Task<GenreDto> EditGenre(UpdateGenreCommand command)
        {
            var genre = await this.Find(command.Id);
            genre.Name = command.Name;
            genre = await this.Update(genre);
            return genre.ToDto();
        }
    }
}
