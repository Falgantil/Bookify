using Bookify.Core;
using Bookify.DataAccess;
using Bookify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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

        public async Task<IHttpActionResult> Get()
        {
            //IEnumerable<Book> books = await _repo.GetAll();
            //if (books == null)
            //{
            //    return NotFound();
            //}
            //return Ok(books);
            var books = new List<Book>
            {
                new Book { Id = 1, Title = "Bob på nye eventyr" },
                new Book { Id = 2, Title = "HarrAr potter" }
            };
            return Ok(books);
        }
    }
}
