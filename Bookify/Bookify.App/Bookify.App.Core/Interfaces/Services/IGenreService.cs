using Bookify.Common.Filter;
using Bookify.Common.Models;

namespace Bookify.App.Core.Interfaces.Services
{
    /// <summary>
    /// The Genre Service interface. 
    /// Inherits from <see cref="IGetByFilterService{TDto,TFilter}"/>, 
    /// and contains extra methods relevant for the Genre Service.
    /// </summary>
    /// <seealso cref="Services.IGetByFilterService{GenreDto, GenreFilter}" />
    public interface IGenreService : IGetByFilterService<GenreDto, GenreFilter>
    {
    }
}