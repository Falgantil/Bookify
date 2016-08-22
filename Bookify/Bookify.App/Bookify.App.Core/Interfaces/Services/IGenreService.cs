using System.Collections.Generic;
using System.Threading.Tasks;

using Bookify.Common.Filter;
using Bookify.Common.Models;

namespace Bookify.App.Core.Interfaces.Services
{
    public interface IGenreService : IGetByFilterService<GenreDto, GenreFilter>
    {
    }
}