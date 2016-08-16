using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Bookify.Core.Interfaces;
using Bookify.Models;

namespace Bookify.API.Controllers
{
    public class GenresController : ApiController
    {
        private IGenreRepository _genreRepository;
        public GenresController(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            return Ok(await _genreRepository.GetAll());
        }

        [HttpPut]
        [Authorize]
        public async Task<IHttpActionResult> Create(Genre genre)
        {
            return Ok(await _genreRepository.Add(genre));
        }

        [HttpPost]
        [Authorize]

        public async Task<IHttpActionResult> Update(Genre genre)
        {
            return Ok(await _genreRepository.Update(genre));
        }
    }
}
