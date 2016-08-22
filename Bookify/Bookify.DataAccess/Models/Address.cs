namespace Bookify.DataAccess.Models
{
    public class Address
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string Country { get; set; }
        public string Street { get; set; }
        public int ZipCode { get; set; }

        public Person Person { get; set; }
    }
}