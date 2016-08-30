using System.Collections.Generic;
using Bookify.Common.Commands.Auth;
using Bookify.Common.Filter;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Bookify.API.Attributes;
using Bookify.Common.Models;
using Bookify.Common.Repositories;

namespace Bookify.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Bookify.API.Controllers.BaseApiController" />
    [RoutePrefix("genres")]
    public class GenresController : BaseApiController
    {
        private readonly IGenreRepository _genreRepository;
        /// <summary>
        /// Initializes a new instance of the <see cref="GenresController"/> class.
        /// </summary>
        /// <param name="genreRepository">The genre repository.</param>
        public GenresController(IGenreRepository genreRepository)
        {
            this._genreRepository = genreRepository;
        }

        /// <summary>
        /// Gets the genres specified by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <response code="200" cref="Get(Bookify.Common.Filter.GenreFilter)">OK</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(IEnumerable<GenreDto>))]
        public async Task<IHttpActionResult> Get([FromUri] GenreFilter filter = null)
        {
            filter = filter ?? new GenreFilter();
            return await this.Try(() => this._genreRepository.GetByFilter(filter));
        }

        /// <summary>
        /// Gets the genres specified by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <response code="200" cref="Get(int)">OK</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ResponseType(typeof(GenreDto))]
        public async Task<IHttpActionResult> Get(int id)
        {
            return await this.Try(() => this._genreRepository.GetById(id));
        }

        /// <summary>
        /// Creates the genre specified by command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <response code="201" cref="Create">OK</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns></returns>
        [HttpPost]
        [Auth]
        [Route("")]
        [ResponseType(typeof(GenreDto))]
        public async Task<IHttpActionResult> Create([FromBody]CreateGenreCommand command)
        {
            return await this.TryCreate(async () => await this._genreRepository.CreateGenre(command));
        }

        /// <summary>
        /// Updates the genre specified by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="command">The command.</param>
        /// <response code="200" cref="Update">OK</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns></returns>
        [HttpPatch]
        [Auth]
        [Route("{id}")]
        [ResponseType(typeof(GenreDto))]
        public async Task<IHttpActionResult> Update(int id, [FromBody]EditGenreCommand command)
        {
            return await this.Try(async () => await this._genreRepository.EditGenre(id, command));
        }
    }
}
