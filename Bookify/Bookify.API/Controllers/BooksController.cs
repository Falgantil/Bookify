using Bookify.Common.Commands.Auth;
using Bookify.Common.Enums;
using Bookify.Common.Filter;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using Bookify.API.Attributes;
using Bookify.Common.Exceptions;
using Bookify.Common.Models;
using Bookify.Common.Repositories;
using Bookify.DataAccess.Models;

namespace Bookify.API.Controllers
{
    [RoutePrefix("books")]
    public class BooksController : BaseApiController
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBookHistoryRepository _bookHistoryRepository;
        private readonly IBookFeedbackRepository _bookFeedbackRepository;
        private readonly IAuthenticationRepository _authRepo;
        private readonly IPersonRepository _personRepository;
        private readonly IBookOrderRepository _bookOrderRepository;

        public BooksController(IBookRepository bookRepository, IBookHistoryRepository bookHistoryRepository, IBookFeedbackRepository bookFeedbackRepository, IAuthenticationRepository authRepo, IPersonRepository personRepository, IBookOrderRepository bookOrderRepository)
        {
            this._bookRepository = bookRepository;
            this._bookHistoryRepository = bookHistoryRepository;
            this._bookFeedbackRepository = bookFeedbackRepository;
            this._authRepo = authRepo;
            this._personRepository = personRepository;
            this._bookOrderRepository = bookOrderRepository;
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get([FromUri]BookFilter filter = null)
        {
            filter = filter ?? new BookFilter();
            return await this.Try(() => this._bookRepository.GetByFilter(filter));
        }

        [HttpGet]
        [Auth]
        [Route("mybooks")]
        public async Task<IHttpActionResult> MyBooks([FromUri]BookFilter filter = null)
        {
            filter = filter ?? new BookFilter();
            var person = await this.GetAuthorizedMember(_authRepo);
            return await this.Try(() => this._bookRepository.GetByFilter(filter, person.PersonDto.Id));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            return await this.Try(() => this._bookRepository.GetById(id));
        }

        [HttpPost]
        [Auth]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody]CreateBookCommand command)
        {
            return await this.TryCreate(() => this._bookRepository.CreateBook(command));
        }

        [HttpPost]
        [Auth]
        [Route("{id}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody]UpdateBookCommand command)
        {
            return await this.Try(() => this._bookRepository.EditBook(id, command));
        }

        [HttpDelete]
        [Auth]
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
        [Auth]
        [Route("{id}/history")]
        public async Task<IHttpActionResult> History(int id)
        {
            return await this.Try(() => this._bookHistoryRepository.GetHistoryForBook(id));
        }



        [HttpPut]
        [Route("{id}/buy")]
        public async Task<IHttpActionResult> Buy(int id, [FromUri]string email)
        {
            return await this.Try(
                async () =>
                    {
                        PersonDto dto;
                        try
                        {
                            var personAuthDto = await this.GetAuthorizedMember(this._authRepo);
                            dto = personAuthDto.PersonDto;
                            if (email != dto.Email) throw new BadRequestException("The email was not identical with the email of the person logged in");
                        }
                        catch (InvalidAccessTokenException)
                        {
                            dto = await this._personRepository.CreatePersonIfNotExists(email);
                        }

                        var command = new CreateOrderCommand
                        {
                            BookId = id,
                            Status = BookOrderStatus.Sold,
                            PersonId = dto.Id
                        };
                        await this._bookOrderRepository.CreateOrder(command);
                    });
        }

        [HttpPost]
        [Auth]
        [Route("{id}/review")]
        public async Task<IHttpActionResult> Review(int id, CreateFeedbackCommand command)
        {
            var personAuthDto = await this.GetAuthorizedMember(this._authRepo);
            return await this.Try(() => this._bookFeedbackRepository.CreateFeedback(id, personAuthDto.PersonDto.Id, command));
        }


        [HttpPut]
        [Auth]
        [Route("{id}/borrow")]
        public async Task<IHttpActionResult> Borrow(int id)
        {
            return await this.Try(
                async () =>
                {
                    var personAuthDto = await this.GetAuthorizedMember(this._authRepo);
                    var dto = personAuthDto.PersonDto;

                    var command = new CreateOrderCommand
                    {
                        BookId = id,
                        Status = BookOrderStatus.Borrowed,
                        PersonId = dto.Id
                    };
                    await this._bookOrderRepository.CreateOrder(command);
                });
        }

        [HttpGet]
        [Auth]
        [Route("{id}/statistics")]
        public async Task<IHttpActionResult> Statistics(int id)
        {
            return await this.Try(async () => await this._bookRepository.FindForStatistics(id));
        }
    }
}
