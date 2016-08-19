using Bookify.Common.Commands.Auth;
using Bookify.Common.Filter;
using System.Threading.Tasks;
using System.Web.Http;
using Bookify.API.Attributes;
using Bookify.Common.Repositories;

namespace Bookify.API.Controllers
{
    [RoutePrefix("genres")]
    public class GenresController : BaseApiController
    {
        private IGenreRepository genreRepository;
        public GenresController(IGenreRepository genreRepository)
        {
            this.genreRepository = genreRepository;
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get([FromUri] GenreFilter filter = null)
        {
            filter = filter ?? new GenreFilter();
            return await this.Try(() => this.genreRepository.GetByFilter(filter));
        }

        [HttpPut]
        [Auth]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody]CreateGenreCommand command)
        {
            return await this.Try(async () => await this.genreRepository.CreateGenre(command));
        }

        [HttpPost]
        [Auth]
        [Route("{id}")]
        public async Task<IHttpActionResult> Update(int id, UpdateGenreCommand command)
        {
            command.Id = id;
            return await this.Try(async () => await this.genreRepository.EditGenre(command));
        }
    }
}
