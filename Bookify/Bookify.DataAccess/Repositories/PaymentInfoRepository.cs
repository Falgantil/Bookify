using Bookify.Core;
using Bookify.Models;

namespace Bookify.DataAccess.Repositories
{
    internal class PaymentInfoRepository : GenericRepository<PaymentInfo>, IPaymentInfoRepository
    {
        internal PaymentInfoRepository(BookifyContext ctx) : base(ctx)
        {

        }
    }
}
