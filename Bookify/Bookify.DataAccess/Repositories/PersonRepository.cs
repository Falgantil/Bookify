using System;
using System.Threading.Tasks;
using Bookify.Core;
using Bookify.Models;
using System.Linq;

namespace Bookify.DataAccess.Repositories
{
    internal class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        internal PersonRepository(BookifyContext ctx) : base(ctx)
        {

        }

        public async Task<Person> CreatePersonIfNotExists(string email)
        {
            var person = (await Get(t => t.Email == email)).FirstOrDefault();

            // Create Email in db if customer is anonymous
            if (person == null)
            {
                person = new Person()
                {
                    Email = email
                };
                await Add(person);
                await SaveChanges();
                // Get the Id the person was asigned when it was created
            }
            return person;
        }
    }
}
