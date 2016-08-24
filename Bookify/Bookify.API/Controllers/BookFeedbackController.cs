using System.Threading.Tasks;
using System.Web.Http;
using Bookify.API.Attributes;
using Bookify.Common.Commands.Auth;
using Bookify.Common.Filter;
using Bookify.Common.Repositories;

namespace Bookify.API.Controllers
{
    [RoutePrefix("feedbacks")]
    public class BookFeedbackController : BaseApiController
    {
        private readonly IBookFeedbackRepository _bookFeedbackRepository;
        private readonly IAuthenticationRepository _authRepo;

        public BookFeedbackController(IBookFeedbackRepository bookFeedbackRepository, IAuthenticationRepository authRepo)
        {
            _bookFeedbackRepository = bookFeedbackRepository;
            this._authRepo = authRepo;
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get([FromUri]FeedbackFilter filter = null)
        {
            filter = filter ?? new FeedbackFilter();
            return await this.Try(() => this._bookFeedbackRepository.GetByFilter(filter));
        }

        [HttpPost]
        [Auth]
        [Route("{id}")]
        public async Task<IHttpActionResult> Create(int id, CreateFeedbackCommand command)
        {
            var personAuthDto = await this.GetAuthorizedMember(this._authRepo);
            return await this.Try(() => this._bookFeedbackRepository.CreateFeedback(id, personAuthDto.PersonDto.Id, command));
        }
        [HttpPut]
        [Auth]
        [Route("{id}")]
        public async Task<IHttpActionResult> Update(int id, UpdateFeedbackCommand command)
        {
            var personAuthDto = await this.GetAuthorizedMember(this._authRepo);
            return await this.Try(() => this._bookFeedbackRepository.EditFeedback(id, personAuthDto.PersonDto.Id, command));
        }
        [HttpDelete]
        [Auth]
        [Route("{id}/delete")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var personAuthDto = await this.GetAuthorizedMember(this._authRepo);
            return await this.Try(() => this._bookFeedbackRepository.DeleteFeedback(id, personAuthDto.PersonDto.Id));
        }
    }
}
