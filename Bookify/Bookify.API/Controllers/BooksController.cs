using Bookify.Models;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

using Bookify.Common.Commands.Auth;
using Bookify.Common.Enums;
using Bookify.Common.Exceptions;
using Bookify.Common.Filter;
using Bookify.Common.Models;
using Bookify.DataAccess.Interfaces.Repositories;
using Bookify.DataAccess.Models;
using Bookify.DataAccess.Models.ViewModels;

namespace Bookify.API.Controllers
{
    [RoutePrefix("books")]
    public class BooksController : BaseApiController
    {
        private readonly IBookRepository bookRepository;
        private readonly IBookHistoryRepository bookHistoryRepository;
        private readonly IPersonRepository personRepository;
        private readonly IBookOrderRepository bookOrderRepository;
        private readonly IBookContentRepository bookContentRepository;
        private readonly IBookFeedbackRepository bookFeedbackRepository;
        private readonly IFileServerRepository fileServerRepository;

        public BooksController(IBookRepository bookRepository, IBookHistoryRepository bookHistoryRepository, IPersonRepository personRepository, IBookOrderRepository bookOrderRepository, IBookContentRepository bookContentRepository, IBookFeedbackRepository bookFeedbackRepository, IFileServerRepository fileServerRepository)
        {
            this.bookRepository = bookRepository;
            this.bookHistoryRepository = bookHistoryRepository;
            this.personRepository = personRepository;
            this.bookOrderRepository = bookOrderRepository;
            this.bookContentRepository = bookContentRepository;
            this.bookFeedbackRepository = bookFeedbackRepository;
            this.fileServerRepository = fileServerRepository;
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get([FromUri]BookFilter filter = null)
        {
            filter = filter ?? new BookFilter();
            return await this.Try(() => this.bookRepository.GetByFilter(filter));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            return await this.Try(() => this.bookRepository.GetById(id));
        }

        [HttpPost]
        [Authorize]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody]CreateBookCommand command)
        {
            return await this.Try(() => this.bookRepository.CreateBook(command));
        }

        [HttpPut]
        [Authorize]
        [Route("{id}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody]UpdateBookCommand command)
        {
            command.BookId = id;
            return await this.Try(() => this.bookRepository.EditBook(command));
        }

        [HttpDelete]
        [Authorize]
        [Route("{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var bookHistory = new BookHistory()
            {
                BookId = id,
                Type = BookHistoryType.Deleted,
                Created = DateTime.Now
            };
            return await this.Try(() => this.bookHistoryRepository.AddHistory(bookHistory));
        }

        [HttpGet]
        [Authorize]
        [Route("{id}/history")]
        public async Task<IHttpActionResult> History(int id)
        {
            return await this.Try(() => this.bookHistoryRepository.GetHistoryForBook(id));
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

        [HttpGet]
        [Authorize]
        [Route("{id}/download")]
        public async Task<IHttpActionResult> Download(int id)
        {
            return await this.Try(
                async () =>
                    {
                        var content = await this.bookContentRepository.GetById(id);
                        return content.EpubPath;
                    });
        }

        [HttpPost]
        [Authorize]
        [Route("{id}/review")]
        public async Task<IHttpActionResult> Review(int id, CreateFeedbackCommand command)
        {
            return await this.Try(() => this.bookFeedbackRepository.CreateFeedback(id, command));
        }

        [HttpGet]
        [Authorize]
        [Route("{id}/read")]
        public async Task<IHttpActionResult> Read(int id)
        {
            // stream the Epub?
            return await this.Download(id);
        }

        [HttpGet]
        [Authorize]
        [Route("{id}/statistics")]
        public async Task<IHttpActionResult> Statistics(int id)
        {
            return await this.Try(async () => await this.bookRepository.FindForStatistics(id));
        }

        [HttpGet]
        [Route("{id}/cover")]
        public async Task<IHttpActionResult> Cover(int id, [FromUri]int? width = null, [FromUri]int? height = null)
        {
            return await this.TryRaw(
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
                async () =>
                    {
                        var stream = this.fileServerRepository.GetCoverFile(id);
                        if (stream == null)
                        {
                            throw new NotFoundException("The specified book does not contain a cover image.");
                        }

                        using (var ms = new MemoryStream())
                        using (var img = Image.FromStream(stream))
                        {

                            var thumbnail = img.GetThumbnailImage(
                                width ?? img.Width,
                                height ?? img.Height,
                                null,
                                new IntPtr());
                            thumbnail.Save(ms, ImageFormat.Png);

                            var response = new HttpResponseMessage(HttpStatusCode.OK)
                            {
                                Content =
                                    new ByteArrayContent(ms.ToArray())
                                    {
                                        Headers = { ContentType = new MediaTypeHeaderValue("image/png") }
                                    }
                            };
                            return this.ResponseMessage(response);
                        }
                    });
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        }
    }
}
