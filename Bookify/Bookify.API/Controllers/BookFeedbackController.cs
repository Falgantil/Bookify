using System.Threading.Tasks;
using System.Web.Http;
using Bookify.Common.Commands.Auth;
using Bookify.Common.Filter;
using Bookify.Common.Repositories;

namespace Bookify.API.Controllers
{
    [RoutePrefix("feedbacks")]
    public class BookFeedbackController : BaseApiController
    {
        private readonly IBookFeedbackRepository _bookFeedbackRepository;

        public BookFeedbackController(IBookFeedbackRepository bookFeedbackRepository)
        {
            _bookFeedbackRepository = bookFeedbackRepository;
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get([FromUri]FeedbackFilter filter = null)
        {
            filter = filter ?? new FeedbackFilter();
           return await this.Try(() => this._bookFeedbackRepository.GetByFilter(filter));
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> Create(int id, CreateFeedbackCommand command)
        {
            return await this.Try(() => this._bookFeedbackRepository.CreateFeedback(id, command));
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> Update(int id, UpdateFeedbackCommand command)
        {
            return await this.Try(() => this._bookFeedbackRepository.EditFeedback(id,command));
        }
        [HttpGet]
        [Route("{id}/delete")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            return await this.Try(() => this._bookFeedbackRepository.DeleteFeedback(id));
        }
    }
}
