using System.Collections.Generic;

using Bookify.Common.Models;

namespace Bookify.DataAccess.Models
{
    public class Person
    {
        public int Id { get; set; }
        public int? PublisherId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Alias { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public Publisher Publisher { get; set; }
        public ICollection<PersonRole> Roles { get; set; } = new HashSet<PersonRole>();
        public ICollection<Address> Addresses { get; set; } = new HashSet<Address>();
        public ICollection<Subscription> Subscriptions { get; set; } = new HashSet<Subscription>();

        public PersonDto ToDto()
        {
            return new PersonDto
            {
                Id = this.Id,
                FirstName = this.Firstname,
                LastName = this.Lastname,
                Email = this.Email,
                PublisherId = this.Publisher?.Id,
                PublisherName = this.Publisher?.Name
            };
        }
    }
}