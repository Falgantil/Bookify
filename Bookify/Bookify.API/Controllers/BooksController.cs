using Bookify.Core;
using Bookify.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Bookify.API.Controllers
{
    public class BooksController : ApiController
    {
        private IBookRepository _repo;

        public BooksController(IBookRepository repo)
        {
            _repo = repo;
        }

        public BooksController()
        {
            
        }
        
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var books = await _repo.GetAll();
            return Ok(books);
            //return Ok(await Task.Factory.StartNew(() =>
            //{
            //    return new List<Book>
            //    {
            //        new Book { Id = 1, Title = "Bob på nye eventyr" },
            //        new Book { Id = 2, Title = "Harry potter" }
            //    };
            //}));
        }

        [HttpPut]
        [Authorize]
        public async Task<IHttpActionResult> Create(Book book)
        {
            _repo.Add(book);
            return Ok();
        }

        [HttpPost]
        [Authorize]
        public async Task<IHttpActionResult> Update(Book book)
        {
            _repo.Update(book);
            return Ok();
        }

        [HttpPost]
        public async Task<IHttpActionResult> Get(int id)
        {
            return Ok(await _repo.Find(id));
        }

        [HttpDelete]
        [Authorize]
        public async Task<IHttpActionResult> Delete(int id)
        {
            _repo.Disable(id);
            return Ok();
        }

        [HttpGet]
        [Authorize]
        public async Task<IHttpActionResult> History(int id)
        {
            return Ok();
        }

        [HttpPut]
        public async Task<IHttpActionResult> Buy(int id)
        {
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
