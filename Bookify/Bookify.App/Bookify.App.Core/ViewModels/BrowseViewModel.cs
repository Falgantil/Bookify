using Bookify.App.Core.Collections;
using Bookify.App.Core.Interfaces.Services;
using Bookify.Common.Filter;
using Bookify.Common.Models;

namespace Bookify.App.Core.ViewModels
{
    public class BrowseViewModel : BaseViewModel
    {
        /// <summary>
        /// The genre service
        /// </summary>
        private readonly IGenreService genreService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BrowseViewModel"/> class.
        /// </summary>
        /// <param name="genreService">The genre service.</param>
        public BrowseViewModel(IGenreService genreService)
        {
            this.genreService = genreService;
            this.Categories = new ObservableServiceCollection<GenreDto, GenreFilter, IGenreService>(this.genreService);
        }

        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <value>
        /// The categories.
        /// </value>
        public ObservableServiceCollection<GenreDto, GenreFilter, IGenreService> Categories { get; }
    }
}