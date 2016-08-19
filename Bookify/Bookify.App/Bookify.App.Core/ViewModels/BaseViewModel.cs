using System.ComponentModel;
using System.Runtime.CompilerServices;
using Bookify.App.Core.Collections;
using Bookify.App.Sdk.Interfaces;
using Bookify.Common.Filter;
using Bookify.Common.Models;

namespace Bookify.App.Core.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class GenresViewModel : BaseViewModel
    {
        private readonly IGenresApi genresApi;

        public GenresViewModel(IGenresApi genresApi)
        {
            this.genresApi = genresApi;
            this.Genres = new ObservableApiCollection<GenreDto, GenreFilter, IGenresApi>(this.genresApi);
        }

        public ObservableApiCollection<GenreDto, GenreFilter, IGenresApi> Genres { get; }
    }
}