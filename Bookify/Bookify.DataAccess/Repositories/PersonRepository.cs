using System.Threading.Tasks;
using Bookify.Models;
using System.Linq;
using Bookify.Core.Interfaces.Repositories;

namespace Bookify.DataAccess.Repositories
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(BookifyContext ctx) : base(ctx)
        {

        }

        public async Task<Person> CreatePersonIfNotExists(string email)
        {
            var person = (await this.Get(t => t.Email == email)).FirstOrDefault();

            // Create Email in db if customer is anonymous
            if (person == null)
            {
                person = new Person()
                {
                    Email = email
                };
                await this.Add(person);
                // Get the Id the person was asigned when it was created
            }
            return person;
        }
    }
}
