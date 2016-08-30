using Bookify.Common.Commands.Auth;
using Bookify.Common.Enums;
using Bookify.Common.Filter;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Bookify.API.Attributes;
using Bookify.Common.Exceptions;
using Bookify.Common.Models;
using Bookify.Common.Repositories;

namespace Bookify.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Bookify.API.Controllers.BaseApiController" />
    [RoutePrefix("books")]
    public class BooksController : BaseApiController
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBookHistoryRepository _bookHistoryRepository;
        private readonly IAuthenticationRepository _authRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IBookOrderRepository _bookOrderRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="BooksController"/> class.
        /// </summary>
        /// <param name="bookRepository">The book repository. This will be injected.</param>
        /// <param name="bookHistoryRepository">The book history repository. This will be injected.</param>
        /// <param name="authRepository">The authentication repo. This will be injected.</param>
        /// <param name="personRepository">The person repository. This will be injected.</param>
        /// <param name="bookOrderRepository">The book order repository. This will be injected.</param>
        public BooksController(
            IBookRepository bookRepository, 
            IBookHistoryRepository bookHistoryRepository, 
            IAuthenticationRepository authRepository, 
            IPersonRepository personRepository, 
            IBookOrderRepository bookOrderRepository)
        {
            this._bookRepository = bookRepository;
            this._bookHistoryRepository = bookHistoryRepository;
            this._authRepository = authRepository;
            this._personRepository = personRepository;
            this._bookOrderRepository = bookOrderRepository;
        }

        /// <summary>
        /// Gets books from the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <response code="200" cref="Get(Bookify.Common.Filter.BookFilter)">OK</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(IPaginatedEnumerable<BookDto>))]
        public async Task<IHttpActionResult> Get([FromUri]BookFilter filter = null)
        {
            filter = filter ?? new BookFilter();
            return await this.Try(() => this._bookRepository.GetByFilter(filter));
        }

        /// <summary>
        /// Gets the bowword books from the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <response code="200" cref="MyBooks">OK</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns></returns>
        [HttpGet]
        [Auth]
        [Route("mybooks")]
        [ResponseType(typeof(IPaginatedEnumerable<BookDto>))]
        public async Task<IHttpActionResult> MyBooks([FromUri]BookFilter filter = null)
        {
            filter = filter ?? new BookFilter();
            var person = await this.GetAuthorizedMember(this._authRepository);
            return await this.Try(() => this._bookRepository.GetByFilter(filter, person.PersonDto.Id));
        }

        /// <summary>
        /// Gets the book specified by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <response code="200" cref="Get(int)">OK</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ResponseType(typeof(DetailedBookDto))]
        public async Task<IHttpActionResult> Get(int id)
        {
            return await this.Try(() => this._bookRepository.GetById(id));
        }

        /// <summary>
        /// Creates a book specified by the command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <response code="201" cref="Create">OK</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns></returns>
        [HttpPost]
        [Auth]
        [Route("")]
        [ResponseType(typeof(DetailedBookDto))]
        public async Task<IHttpActionResult> Create([FromBody]CreateBookCommand command)
        {
            return await this.TryCreate(() => this._bookRepository.CreateBook(command));
        }

        /// <summary>
        /// Updates the book specified by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="command">The command.</param>
        /// <response code="200" cref="Update">OK</response>
        /// <response code="500">Internal Server Error</response>
        /// <response code="404">Not Found Error</response>
        /// <returns></returns>
        [HttpPatch]
        [Auth]
        [Route("{id}")]
        [ResponseType(typeof(DetailedBookDto))]
        public async Task<IHttpActionResult> Update(int id, [FromBody]EditBookCommand command)
        {
            return await this.Try(() => this._bookRepository.EditBook(id, command));
        }

        /// <summary>
        /// Deletes the book specified by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <response code="200">OK</response>
        /// <response code="500">Internal Server Error</response>
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

        /// <summary>
        /// Gets the Histories specified by book identifier.
        /// </summary>
        /// <param name="id">The book identifier.</param>
        /// <response code="200" cref="History">OK</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns></returns>
        [HttpGet]
        [Auth]
        [Route("{id}/history")]
        [ResponseType(typeof(IEnumerable<BookHistoryDto>))]
        public async Task<IHttpActionResult> History(int id)
        {
            return await this.Try(() => this._bookHistoryRepository.GetHistoryForBook(id));
        }

        /// <summary>
        /// Buys the book specified by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="email">The email.</param>
        /// <response code="200">OK</response>
        /// <response code="500">Internal Server Error</response>
        /// <response code="400">Bad Request Error</response>
        /// <returns></returns>
        [HttpPost]
        [Route("{id}/buy")]
        [ResponseType(typeof(IHttpActionResult))]
        public async Task<IHttpActionResult> Buy(int id, [FromUri]string email)
        {
            return await this.Try(
                async () =>
                    {
                        PersonDto dto;
                        try
                        {
                            var personAuthDto = await this.GetAuthorizedMember(this._authRepository);
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

        /// <summary>
        /// Borrows the book specified by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <response code="200">OK</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns></returns>
        [HttpPost]
        [Auth]
        [Route("{id}/borrow")]
        [ResponseType(typeof(IHttpActionResult))]
        public async Task<IHttpActionResult> Borrow(int id)
        {
            return await this.Try(
                async () =>
                {
                    var personAuthDto = await this.GetAuthorizedMember(this._authRepository);
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

        /// <summary>
        /// Gets the Statisticses specified by the book identifier.
        /// </summary>
        /// <param name="id">The book identifier.</param>
        /// <response code="200" cref="Statistics">OK</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns></returns>
        [HttpGet]
        [Auth]
        [Route("{id}/statistics")]
        [ResponseType(typeof(BookStatisticsDto))]
        public async Task<IHttpActionResult> Statistics(int id)
        {
            return await this.Try(async () => await this._bookRepository.FindForStatistics(id));
        }
    }
}
