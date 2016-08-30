using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Bookify.API.Attributes;
using Bookify.Common.Commands.Auth;
using Bookify.Common.Filter;
using Bookify.Common.Models;
using Bookify.Common.Repositories;

namespace Bookify.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Bookify.API.Controllers.BaseApiController" />
    [RoutePrefix("feedbacks")]
    public class BookFeedbackController : BaseApiController
    {
        private readonly IBookFeedbackRepository _bookFeedbackRepository;
        private readonly IAuthenticationRepository _authRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookFeedbackController"/> class.
        /// </summary>
        /// <param name="bookFeedbackRepository">The book feedback repository.</param>
        /// <param name="authRepository">The authentication repo.</param>
        public BookFeedbackController(IBookFeedbackRepository bookFeedbackRepository, IAuthenticationRepository authRepository)
        {
            this._bookFeedbackRepository = bookFeedbackRepository;
            this._authRepository = authRepository;
        }

        /// <summary>
        /// Gets bookfeedbacks from the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <response code="200" cref="Get">OK</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(IPaginatedEnumerable<BookFeedbackDto>))]
        public async Task<IHttpActionResult> Get([FromUri]FeedbackFilter filter = null)
        {
            filter = filter ?? new FeedbackFilter();
            return await this.Try(() => this._bookFeedbackRepository.GetByFilter(filter));
        }

        /// <summary>
        /// Creates the bookfeedback for specified book identifier and command.
        /// </summary>
        /// <param name="id">The book identifier.</param>
        /// <param name="command">The command.</param>
        /// <response code="201" cref="Create">OK</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns></returns>
        [HttpPost]
        [Auth]
        [Route("{id}")]
        [ResponseType(typeof(BookFeedbackDto))]
        public async Task<IHttpActionResult> Create(int id, CreateFeedbackCommand command)
        {
            var personAuthDto = await this.GetAuthorizedMember(this._authRepository);
            return await this.TryCreate(() => this._bookFeedbackRepository.CreateFeedback(id, personAuthDto.PersonDto.Id, command));
        }

        /// <summary>
        /// Updates bookFeedBack from specified book identifier and command.
        /// </summary>
        /// <param name="id">The book identifier.</param>
        /// <param name="command">The command.</param>
        /// <response code="200" cref="Update">OK</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns></returns>
        [HttpPatch]
        [Auth]
        [Route("{id}")]
        [ResponseType(typeof(BookFeedbackDto))]
        public async Task<IHttpActionResult> Update(int id, EditFeedbackCommand command)
        {
            var personAuthDto = await this.GetAuthorizedMember(this._authRepository);
            return await this.Try(() => this._bookFeedbackRepository.EditFeedback(id, personAuthDto.PersonDto.Id, command));
        }

        /// <summary>
        /// Deletes the bookFeedback specified by book identifier and the current users identifier.
        /// </summary>
        /// <param name="id">The book identifier.</param>
        /// <response code="200">OK</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns></returns>
        [HttpDelete]
        [Auth]
        [Route("{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var personAuthDto = await this.GetAuthorizedMember(this._authRepository);
            return await this.Try(() => this._bookFeedbackRepository.DeleteFeedback(id, personAuthDto.PersonDto.Id));
        }
    }
}
