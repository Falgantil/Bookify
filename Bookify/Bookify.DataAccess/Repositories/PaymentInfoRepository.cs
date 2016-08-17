using Bookify.Core.Interfaces.Repositories;
using Bookify.Models;

namespace Bookify.DataAccess.Repositories
{
    public class PaymentInfoRepository : GenericRepository<PaymentInfo>, IPaymentInfoRepository
    {
        public PaymentInfoRepository(BookifyContext ctx) : base(ctx)
        {

        }
    }
}
