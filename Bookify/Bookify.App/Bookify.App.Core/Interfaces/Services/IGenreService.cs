using System.Collections.Generic;
using System.Threading.Tasks;
using Bookify.Models;

namespace Bookify.App.Core.Interfaces.Services
{
    public interface IGenreService
    {
        Task<IEnumerable<Genre>> GetGenres();
    }
}