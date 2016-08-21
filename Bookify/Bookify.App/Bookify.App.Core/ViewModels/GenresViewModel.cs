using Bookify.App.Core.Collections;
using Bookify.App.Core.Interfaces.Services;
using Bookify.Common.Filter;
using Bookify.Common.Models;

namespace Bookify.App.Core.ViewModels
{
    public class GenresViewModel : BaseViewModel
    {
        private readonly IGenreService service;

        public GenresViewModel(IGenreService service)
        {
            this.service = service;
            this.Genres = new ObservableServiceCollection<GenreDto, GenreFilter, IGenreService>(this.service);
        }

        public ObservableServiceCollection<GenreDto, GenreFilter, IGenreService> Genres { get; }
    }
}