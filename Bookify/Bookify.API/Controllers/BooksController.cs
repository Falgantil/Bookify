using Bookify.Core;
using Bookify.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using Bookify.Core.Interfaces;
using Bookify.Models.ViewModels;
using Bookify.Core.Interfaces.Repositories;

namespace Bookify.API.Controllers
{
    public class BooksController : ApiController
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBookHistoryRepository _bookHistoryRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IBookOrderRepository _bookOrderRepository;
        private readonly IBookContentRepository _bookContentRepository;
        private readonly IBookFeedbackRepository _bookFeedbackRepository;
        private readonly IFileServerRepository _fileServerRepository;
        public BooksController(IBookRepository bookRepository, IBookHistoryRepository bookHistoryRepository, IPersonRepository personRepository, IBookOrderRepository bookOrderRepository, IBookContentRepository bookContentRepository, IBookFeedbackRepository bookFeedbackRepository, IFileServerRepository fileServerRepository)
        {
            _bookRepository = bookRepository;
            _bookHistoryRepository = bookHistoryRepository;
            _personRepository = personRepository;
            _bookOrderRepository = bookOrderRepository;
            _bookContentRepository = bookContentRepository;
            _bookFeedbackRepository = bookFeedbackRepository;
            _fileServerRepository = fileServerRepository;
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get([FromUri]Core.Filter.BookFilter filter)
        {
            // Genres not added yet, other params works though
            var booksQuery = await _bookRepository.GetAllByParams(filter.Index, filter.Count, filter.GenreIds, filter.Search, filter.OrderBy, filter.Desc);
            // search for the books
            return Ok(booksQuery);

            // return the book when done searching 
        }

        [HttpPut]
        [Authorize]
        public async Task<IHttpActionResult> Create(Book book)
        {
            return Ok(await _bookRepository.Add(book));
        }

        [HttpPost]
        [Authorize]
        public async Task<IHttpActionResult> Update(Book book)
        {
            return Ok(await _bookRepository.Update(book));
        }

        [HttpGet]
        public async Task<IHttpActionResult> Book(int id)
        {

            Book book;
            try
            {
                book = await _bookRepository.Find(id);
            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
            return Ok(book);

            //return await CatchExceptions(async () => await _bookRepository.Find(id));
        }


        [HttpDelete]
        [Authorize]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var bookHistory = new BookHistory()
            {
                BookId = id,
                Type = BookHistoryType.Deleted,
                Created = DateTime.Now
            };
            return Ok(await _bookHistoryRepository.Add(bookHistory));
        }

        [HttpGet]
        [Authorize]
        public async Task<IHttpActionResult> History(int id)
        {
            var book = await _bookRepository.Find(id);
            return Ok(book.History);
        }

        [HttpPut]
        public async Task<IHttpActionResult> Buy(int id, string email)
        {
            var person = _personRepository.CreatePersonIfNotExists(email);


            return Ok(await _bookOrderRepository.Add(new BookOrder()
            {
                BookId = id,
                Created = DateTime.Now,
                Status = BookOrderStatus.Sold,
                PersonId = person.Id
            }));
        }

        [HttpGet]
        [Authorize]
        public async Task<IHttpActionResult> Download(int id)
        {
            var book = await _bookContentRepository.Find(id);
            return Content(HttpStatusCode.OK, book.EpubPath);
        }

        [HttpPut]
        [Authorize]
        public async Task<IHttpActionResult> Review(int id, int personId, int rating, string text)
        {

            return Ok(await _bookFeedbackRepository.Add(new BookFeedback()
            {
                BookId = id,
                PersonId = personId,
                Rating = rating,
                Text = text
            }));
        }

        [HttpGet]
        [Authorize]
        public async Task<IHttpActionResult> Read(int id)
        {
            // stream the Epub?
            var book = await _bookContentRepository.Find(id);
            return Content(HttpStatusCode.OK, book.EpubPath);
        }

        [HttpGet]
        [Authorize]
        public async Task<IHttpActionResult> Statistics(int id)
        {
            return Ok( new BookStatistics() { Book = await _bookRepository.FindForStatistics(id) });
        }
        [HttpGet]
        public async Task<HttpResponseMessage> Cover(int id, int? width = null, int? height = null)
        {
            var stream = _fileServerRepository.GetCoverFile(id);
            if (stream == null) return new HttpResponseMessage(HttpStatusCode.InternalServerError);

            using (var ms = new MemoryStream())
            using (var img = Image.FromStream(stream))
            {

                var thumbnail = img.GetThumbnailImage(width ?? img.Width, height ?? img.Height, null, new IntPtr());
                thumbnail.Save(ms, ImageFormat.Png);

                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(ms.ToArray())
                    {
                        Headers =
                        {
                            ContentType = new MediaTypeHeaderValue("image/png")
                        }
                    }
                };
            }
        }

        public async Task<IHttpActionResult> CatchExceptions(Func<Task<Book>> func)
        {
            Book item = null;
            try
            {
                item = await func();
            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
            return Ok(item);
        }

    }
}
