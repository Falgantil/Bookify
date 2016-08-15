using Bookify.Core;
using Bookify.Core.Interfaces;
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
