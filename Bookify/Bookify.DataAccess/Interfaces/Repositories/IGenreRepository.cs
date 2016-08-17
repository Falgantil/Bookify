using System.Threading.Tasks;

using Bookify.Common.Commands.Auth;
using Bookify.Common.Filter;
using Bookify.Common.Models;
using Bookify.DataAccess.Models;
using Bookify.DataAccess.Models.ViewModels;

namespace Bookify.DataAccess.Interfaces.Repositories
{
    public interface IGenreRepository : IGenericRepository<Genre>
    {
        Task<IPaginatedEnumerable<GenreDto>> GetByFilter(GenreFilter filter);

        Task<GenreDto> CreateGenre(CreateGenreCommand command);

        Task<GenreDto> EditGenre(UpdateGenreCommand command);
    }
}
