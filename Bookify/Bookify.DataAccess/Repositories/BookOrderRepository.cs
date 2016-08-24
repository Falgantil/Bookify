using System;
using System.Threading.Tasks;
using Bookify.Common.Commands.Auth;
using Bookify.Common.Repositories;
using Bookify.DataAccess.Models;

namespace Bookify.DataAccess.Repositories
{
    public class BookOrderRepository : GenericRepository<BookOrder>, IBookOrderRepository
    {
        public BookOrderRepository(BookifyContext context) : base(context)
        {

        }

        public async Task CreateOrder(CreateOrderCommand command)
        {
            await this.Add(new BookOrder
            {
                BookId = command.BookId,
                Created = DateTime.Now,
                PersonId = command.PersonId,
                Status = command.Status
            });
        }
    }
}
