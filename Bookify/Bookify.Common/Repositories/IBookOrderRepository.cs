using System.Threading.Tasks;
using Bookify.Common.Commands.Auth;

namespace Bookify.Common.Repositories
{
    public interface IBookOrderRepository
    {
        Task CreateOrder(CreateOrderCommand command);
    }
}
