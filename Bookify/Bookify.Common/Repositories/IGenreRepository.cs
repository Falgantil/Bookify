using System.Collections.Generic;
using System.Threading.Tasks;
using Bookify.Common.Commands.Auth;
using Bookify.Common.Filter;
using Bookify.Common.Models;

namespace Bookify.Common.Repositories
{
    public interface IGenreRepository
    {
        Task<IEnumerable<GenreDto>> GetByFilter(GenreFilter filter);

        Task<GenreDto> CreateGenre(CreateGenreCommand command);

        Task<GenreDto> EditGenre(int id, EditGenreCommand command);

        Task<GenreDto> GetById(int id);
    }
}
