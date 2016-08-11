using System.Collections.Generic;

namespace Bookify.Models
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
        public List<PersonRole> Roles { get; set; }
        public List<Address> Addresses { get; set; }
    }
}