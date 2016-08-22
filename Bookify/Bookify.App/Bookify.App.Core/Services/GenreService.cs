using System.Collections.Generic;
using System.Threading.Tasks;

using Bookify.App.Core.Interfaces.Services;
using Bookify.App.Sdk.Interfaces;
using Bookify.Common.Filter;
using Bookify.Common.Models;

namespace Bookify.App.Core.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenresApi api;

        public GenreService(IGenresApi api)
        {
            this.api = api;
        }

        //public async Task<IEnumerable<GenreDto>> GetGenres()
        //{
        //    await Task.Delay(1500);
        //    return new[]
        //    {
        //        new GenreDto
        //        {
        //            Id = 1,
        //            Name = "Art and Photography"
        //        },
        //        new GenreDto
        //        {
        //            Id = 2,
        //            Name = "Biographies & Memoirs"
        //        },
        //        new GenreDto
        //        {
        //            Id = 3,
        //            Name = "Children's Books"
        //        },
        //        new GenreDto
        //        {
        //            Id = 4,
        //            Name = "Cookbooks, Food & Wine"
        //        },
        //        new GenreDto
        //        {
        //            Id = 5,
        //            Name = "History"
        //        },
        //        new GenreDto
        //        {
        //            Id = 6,
        //            Name = "Literature & Fiction"
        //        },
        //        new GenreDto
        //        {
        //            Id = 7,
        //            Name = "Mysery & Suspense"
        //        },
        //        new GenreDto
        //        {
        //            Id = 8,
        //            Name = "Romance"
        //        },
        //        new GenreDto
        //        {
        //            Id = 9,
        //            Name = "Sci-fi & Fantasy"
        //        },
        //        new GenreDto
        //        {
        //            Id = 10,
        //            Name = "Teen & Young Adult"
        //        },
        //    };
        //}

        public async Task<IPaginatedEnumerable<GenreDto>> GetItems(GenreFilter filter)
        {
            return await this.api.GetGenres(filter);
        }
    }
}