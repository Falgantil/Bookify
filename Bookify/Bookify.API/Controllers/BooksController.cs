using Bookify.Core;
using Bookify.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Linq;

namespace Bookify.API.Controllers
{
    public class BooksController : ApiController
    {
        private IBookRepository _bookRepo;
        private IBookHistoryRepository _bookHistoryRepo;
        private IPersonRepository _personRepository;
        private IBookOrderRepository _bookOrderRepository;

        public BooksController(IBookRepository bookRepo, IBookHistoryRepository bookHistory, IPersonRepository personRepo, IBookOrderRepository bookOrderRepository)
        {
            _bookRepo = bookRepo;
            _bookHistoryRepo = bookHistory;
            _personRepository = personRepo;
            _bookOrderRepository = bookOrderRepository;
        }

        public BooksController()
        {

        }

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var books = await _bookRepo.GetAll();
            return Ok(books);
        }

        [HttpPut]
        [Authorize]
        public async Task<IHttpActionResult> Create(Book book)
        {
            _bookRepo.Add(book);
            return Ok();
        }

        [HttpPost]
        [Authorize]
        public async Task<IHttpActionResult> Update(Book book)
        {
            _bookRepo.Update(book);
            return Ok();
        }

        [HttpPost]
        public async Task<IHttpActionResult> Get(int id)
        {
            return Ok(await _bookRepo.Find(id));
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
            await _bookHistoryRepo.Add(bookHistory);
            await _bookHistoryRepo.SaveChanges();
            return Ok();
        }

        [HttpGet]
        [Authorize]
        public async Task<IHttpActionResult> History(int id)
        {
            var book = await _bookRepo.Find(id);
            return Ok(book.History);
        }

        [HttpPut]
        public async Task<IHttpActionResult> Buy(int id, string email)
        {
            var person = (await _personRepository.Get(t => t.Email == email)).FirstOrDefault();

            // Create Email in db if customer is anonymous
            if (person == null)
            {
                person = new Person()
                {
                    Email = email
                };
                await _personRepository.Add(person);
                await _personRepository.SaveChanges();
                // Get the Id the person was asigned when it was created
            }





            await _bookOrderRepository.Add(new BookOrder()
            {
                BookId = id,
                Created = DateTime.Now,
                Status = BookOrderStatus.Sold,
                PersonId = person.Id
            });
            await _bookOrderRepository.SaveChanges();

            return Ok();
        }

        [HttpGet]
        [Authorize]
        public async Task<IHttpActionResult> Download(int id)
        {
            return Ok();
        }

        [HttpPut]
        [Authorize]
        public async Task<IHttpActionResult> Review(int id)
        {
            return Ok();
        }

        [HttpGet]
        [Authorize]
        public async Task<IHttpActionResult> Read(int id)
        {
            return Ok();
        }

        [HttpGet]
        [Authorize]
        public async Task<IHttpActionResult> Statistics(int id)
        {
            return Ok();
        }
    }
}
