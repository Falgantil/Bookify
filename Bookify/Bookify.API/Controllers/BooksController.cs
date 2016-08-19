using Bookify.Common.Commands.Auth;
using Bookify.Common.Enums;
using Bookify.Common.Filter;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using Bookify.API.Attributes;
using Bookify.Common.Repositories;
using Bookify.DataAccess.Models;

namespace Bookify.API.Controllers
{
    [RoutePrefix("books")]
    public class BooksController : BaseApiController
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBookHistoryRepository _bookHistoryRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IBookOrderRepository _bookOrderRepository;
        private readonly IBookFeedbackRepository _bookFeedbackRepository;
        private readonly IFileServerRepository _fileServerRepository;

        public BooksController(IBookRepository bookRepository, IBookHistoryRepository bookHistoryRepository, IPersonRepository personRepository, IBookOrderRepository bookOrderRepository, IBookFeedbackRepository bookFeedbackRepository, IFileServerRepository fileServerRepository)
        {
            this._bookRepository = bookRepository;
            this._bookHistoryRepository = bookHistoryRepository;
            this._personRepository = personRepository;
            this._bookOrderRepository = bookOrderRepository;
            this._bookFeedbackRepository = bookFeedbackRepository;
            this._fileServerRepository = fileServerRepository;
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get([FromUri]BookFilter filter = null)
        {
            filter = filter ?? new BookFilter();
            return await this.Try(() => this._bookRepository.GetByFilter(filter));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            return await this.Try(() => this._bookRepository.GetById(id));
        }

        [HttpPost]
        [Authorize]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody]CreateBookCommand command)
        {
            return await this.Try(() => this._bookRepository.CreateBook(command));
        }

        [HttpPut]
        [Authorize]
        [Route("{id}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody]UpdateBookCommand command)
        {
            return await this.Try(() => this._bookRepository.EditBook(id, command));
        }

        [HttpDelete]
        [Authorize]
        [Route("{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var command = new CreateHistoryCommand
            {
                BookId = id,
                Type = BookHistoryType.Deleted,
                Created = DateTime.Now
            };
            return await this.Try(() => this._bookHistoryRepository.AddHistory(command));
        }

        [HttpGet]
        [Authorize]
        [Route("{id}/history")]
        public async Task<IHttpActionResult> History(int id)
        {
            return await this.Try(() => this._bookHistoryRepository.GetHistoryForBook(id));
        }

       

        //[HttpPut]
        //[Route("{id}/buy")]
        //public async Task<IHttpActionResult> Buy(int id, string email)
        //{
        //    return await this.Try(
        //        async () =>
        //            {
        //                var person = this.personRepository.CreatePersonIfNotExists(email);

        //                return
        //                    await
        //                    this.bookOrderRepository.Add(
        //                        new BookOrder()
        //                        {
        //                            BookId = id,
        //                            Created = DateTime.Now,
        //                            Status = BookOrderStatus.Sold,
        //                            PersonId = person.Id
        //                        });
        //            });
        //}

        //[HttpGet]
        //[Authorize]
        //[Route("{id}/download")]
        //public async Task<IHttpActionResult> Download(int id)
        //{
        //    return await this.Try(
        //        async () =>
        //            {
        //                var content = await this.bookContentRepository.GetById(id);
        //                return content.EpubPath;
        //            });
        //}

        [HttpPost]
        [Authorize]
        [Route("{id}/review")]
        public async Task<IHttpActionResult> Review(int id, CreateFeedbackCommand command)
        {
            return await this.Try(() => this._bookFeedbackRepository.CreateFeedback(id, command));
        }

        //[HttpGet]
        //[Authorize]
        //[Route("{id}/read")]
        //public async Task<IHttpActionResult> Read(int id)
        //{
        //    // stream the Epub?
        //    return await this.Download(id);
        //}

        [HttpGet]
        [Authorize]
        [Route("{id}/statistics")]
        public async Task<IHttpActionResult> Statistics(int id)
        {
            return await this.Try(async () => await this._bookRepository.FindForStatistics(id));
        }
    }
}
