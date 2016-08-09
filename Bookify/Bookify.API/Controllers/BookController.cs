using Bookify.Core;
using Bookify.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace Bookify.API.Controllers
{
    public class BookController : ApiController
    {
        private IBookifyRepository _repo;

        public BookController(IBookifyRepository repo)
        {
            _repo = repo;            
        }

        public string GetAllBooks()
        {
            return Json<IEnumerable<Book>>(_repo.BookRepository.GetAll()).ToString();
        }


    }
}
