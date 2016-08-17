using System.Threading.Tasks;
using System.Web.Http;
using Bookify.Core.Interfaces.Repositories;
using Bookify.Models;

namespace Bookify.API.Controllers
{
    [Authorize]
    public class PublishersController : ApiController
    {
        private IPublisherRepository _publisherRepository;
        public PublishersController(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            return Ok(await _publisherRepository.GetAll());
        }

        [HttpPut]
        public async Task<IHttpActionResult> Create(Publisher publisher)
        {
            return Ok(await _publisherRepository.Add(publisher));
        }

        [HttpPost]
        public async Task<IHttpActionResult> Update(Publisher publisher)
        {
            return Ok(await _publisherRepository.Update(publisher));
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            return Ok(await _publisherRepository.Find(id));
        }
    }
}
