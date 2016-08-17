using Bookify.DataAccess.Interfaces.Repositories;
using Bookify.DataAccess.Models;

namespace Bookify.DataAccess.Repositories
{
    public class PaymentInfoRepository : GenericRepository<PaymentInfo>, IPaymentInfoRepository
    {
        public PaymentInfoRepository(BookifyContext context) : base(context)
        {

        }
    }
}
