using System.Threading.Tasks;
using Bookify.Common.Commands.Auth;
using Bookify.Common.Filter;
using Bookify.Common.Models;

namespace Bookify.Common.Repositories
{
    public interface IGenreRepository
    {
        Task<IPaginatedEnumerable<GenreDto>> GetByFilter(GenreFilter filter);

        Task<GenreDto> CreateGenre(CreateGenreCommand command);

        Task<GenreDto> EditGenre(UpdateGenreCommand command);
    }
}
