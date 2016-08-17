using System;

namespace Bookify.DataAccess.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Expires { get; set; }
        public decimal Paid { get; set; }

        public Person Person { get; set; }
    }
}
