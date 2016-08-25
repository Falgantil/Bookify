using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Linq;

using Bookify.Common.Commands.Auth;
using Bookify.Common.Exceptions;
using Bookify.Common.Models;
using Bookify.Common.Repositories;
using Bookify.DataAccess.Models;

namespace Bookify.DataAccess.Repositories
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(BookifyContext context) : base(context)
        {

        }

        public async Task<PersonDto> CreatePersonIfNotExists(string email)
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
            return person.ToDto();
        }

        public async Task<PersonDto> GetByEmail(string email)
        {
            var people = await this.Get(p => p.Email == email);
            var person = await people.SingleAsync();
            return person.ToDto();
        }

        public async Task<PersonDto> CreatePerson(CreateAccountCommand command)
        {
            var person = await this.GetByEmail(command.Email);
            if (person != null)
            {
                throw new BadRequestException("An account already exists with the specified email.");
            }

            var entity = new Person
            {
                Firstname = command.FirstName,
                Lastname = command.LastName,
                Email = command.Email,
                Password = EncryptSha512.GetPassword(command.Password),
                Alias = command.Username
            };
            entity = await this.Add(entity);
            return entity.ToDto();
        }

        public async Task<PersonDto> GetById(int userId)
        {
            var person = await this.Find(userId);
            return person.ToDto();
        }

        public async Task<PersonDto> EditPerson(int id, EditPersonCommand command)
        {
            var person = await this.Find(id);
            person.Firstname = command.FirstName ?? person.Firstname;
            person.Lastname = command.LastName ?? person.Lastname;
            person.Alias = command.Username ?? person.Alias;
            if (!string.IsNullOrEmpty(command.Password))
            {
                person.Password = EncryptSha512.GetPassword(command.Password);
            }
            person = await this.Update(person);
            return person.ToDto();
        }

        public async Task Subscribe(int personId)
        {
            if (await HasSubscription(personId)) throw new BadRequestException($"The personId: {personId}, already has a subscription");
            var subscription = new Subscription
            {
                PersonId = personId,
                Created = DateTime.Now,
                Expires = DateTime.Now.AddDays(30),
                Paid = 0
            };

            this.Context.Subscriptions.Add(subscription);
            await this.Context.SaveChangesAsync();
        }

        public async Task<bool> HasSubscription(int personId)
        {
            return await this.Context.Subscriptions.AnyAsync(
                    x => x.PersonId == personId && x.Expires < DateTime.Now);
        }
    }
}
