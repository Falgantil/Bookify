namespace Bookify.Models
{
    public class PaymentInfo
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public bool Primary { get; set; }

        public Person Person { get; set; }
    }
}
