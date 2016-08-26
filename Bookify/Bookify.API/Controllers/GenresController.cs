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
        private readonly IGenreRepository genreRepository;
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

        [HttpGet]
        [Route("[id]")]
        public async Task<IHttpActionResult> Get(int id)
        {
            return await this.Try(() => this.genreRepository.GetById(id));
        }

        [HttpPost]
        [Auth]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody]CreateGenreCommand command)
        {
            return await this.Try(async () => await this.genreRepository.CreateGenre(command));
        }

        [HttpPatch]
        [Auth]
        [Route("{id}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody]EditGenreCommand command)
        {
            return await this.Try(async () => await this.genreRepository.EditGenre(id, command));
        }
    }
}
