using Bookify.App.Core.Collections;
using Bookify.App.Core.Interfaces.Services;
using Bookify.Common.Filter;
using Bookify.Common.Models;

namespace Bookify.App.Core.ViewModels
{
    public class GenresViewModel : BaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenresViewModel"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        public GenresViewModel(IGenreService service)
        {
            this.Genres = new ObservableServiceCollection<GenreDto, GenreFilter, IGenreService>(service);
        }

        /// <summary>
        /// Gets the genres.
        /// </summary>
        /// <value>
        /// The genres.
        /// </value>
        public ObservableServiceCollection<GenreDto, GenreFilter, IGenreService> Genres { get; }
    }
}