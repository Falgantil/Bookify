using Bookify.Common.Models;

namespace Bookify.DataAccess.Models
{
    public class PersonRole
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string Name { get; set; }

        public Person Person { get; set; }

        public PersonRoleDto ToPersonRoleDto()
        {
            return new PersonRoleDto()
            {
                Id = Id,
                Name = Name

            };
        }
    }
}