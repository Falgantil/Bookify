namespace Bookify.Models
{
    public class Address
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string Contry { get; set; }
        public string Street { get; set; }
        public int ZipCode { get; set; }

        public Person Person { get; set; }
    }
}