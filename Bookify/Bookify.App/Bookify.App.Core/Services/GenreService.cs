using System.Collections.Generic;
using System.Threading.Tasks;
using Bookify.App.Core.Interfaces.Services;
using Bookify.Models;

namespace Bookify.App.Core.Services
{
    public class GenreService : IGenreService
    {
        public async Task<IEnumerable<Genre>> GetGenres()
        {
            await Task.Delay(1500);
            return new[]
            {
                new Genre
                {
                    Id = 1,
                    Name = "Art and Photography"
                },
                new Genre
                {
                    Id = 2,
                    Name = "Biographies & Memoirs"
                },
                new Genre
                {
                    Id = 3,
                    Name = "Children's Books"
                },
                new Genre
                {
                    Id = 4,
                    Name = "Cookbooks, Food & Wine"
                },
                new Genre
                {
                    Id = 5,
                    Name = "History"
                },
                new Genre
                {
                    Id = 6,
                    Name = "Literature & Fiction"
                },
                new Genre
                {
                    Id = 7,
                    Name = "Mysery & Suspense"
                },
                new Genre
                {
                    Id = 8,
                    Name = "Romance"
                },
                new Genre
                {
                    Id = 9,
                    Name = "Sci-fi & Fantasy"
                },
                new Genre
                {
                    Id = 10,
                    Name = "Teen & Young Adult"
                },
            };
        }
    }
}