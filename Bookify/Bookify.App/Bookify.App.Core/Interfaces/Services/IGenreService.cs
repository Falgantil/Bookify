using System.Collections.Generic;
using System.Threading.Tasks;
using Bookify.Common.Models;

namespace Bookify.App.Core.Interfaces.Services
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreDto>> GetGenres();
    }
}