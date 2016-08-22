using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Bookify.App.Core.Collections;
using Bookify.App.Core.Interfaces.Services;
using Bookify.App.Core.Services;
using Bookify.Common.Filter;
using Bookify.Common.Models;

namespace Bookify.App.Core.ViewModels
{
    public class BrowseViewModel : BaseViewModel
    {
        private readonly IGenreService genreService;

        public BrowseViewModel(IGenreService genreService)
        {
            this.genreService = genreService;
            this.Categories = new ObservableServiceCollection<GenreDto, GenreFilter, IGenreService>(this.genreService);
        }

        public ObservableServiceCollection<GenreDto, GenreFilter, IGenreService> Categories { get; }
    }
}