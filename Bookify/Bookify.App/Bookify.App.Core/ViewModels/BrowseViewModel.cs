﻿using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Bookify.App.Core.Interfaces.Services;
using Bookify.Common.Models;

namespace Bookify.App.Core.ViewModels
{
    public class BrowseViewModel : BaseViewModel
    {
        private readonly IGenreService genreService;

        public BrowseViewModel(IGenreService genreService)
        {
            this.genreService = genreService;
        }

        public ObservableCollection<GenreDto> Categories { get; } = new ObservableCollection<GenreDto>();

        public async Task LoadItems()
        {
            var genres = await this.genreService.GetGenres();
            foreach (var genre in genres)
            {
                this.Categories.Add(genre);
            }
        }
    }
}