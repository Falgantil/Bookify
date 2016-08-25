using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Bookify.Common.Commands.Auth;
using Bookify.Common.Enums;
using Bookify.Common.Exceptions;
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
            string msg = $"The book with the id: {command.BookId} has already been borrowed or queued by the personId: {command.PersonId}";
            if (command.Status == BookOrderStatus.Borrowed)
            {
                // Get all orders where bookId is the book we are searching for & the date is valid (returndate has to be more than current date)
                var orders = Context.BookOrders.Where(x => x.BookId == command.BookId && x.ReturnDatetime > DateTime.Now || x.ReturnDatetime == null);
                // Check if person already has a valid BookOrder 
                if (orders.Where(x => x.Status == BookOrderStatus.Borrowed || x.Status == BookOrderStatus.Queued).Any(x => x.PersonId == command.PersonId))
                    throw new BadRequestException(msg);
                var book = await Context.Books.Where(x => x.Id == command.BookId).SingleAsync();
                // if amount of copies is less or equal to amount of orders, borrow it, else queue it
                if (book.CopiesAvailable > orders.Count(x => x.Status == BookOrderStatus.Borrowed))
                {
                    await this.Add(new BookOrder
                    {
                        BookId = command.BookId,
                        Created = DateTime.Now,
                        PersonId = command.PersonId,
                        ReturnDatetime = DateTime.Now.AddDays(30),
                        Status = BookOrderStatus.Borrowed
                    });
                }
                else
                {
                    await this.Add(new BookOrder
                    {
                        BookId = command.BookId,
                        Created = DateTime.Now,
                        PersonId = command.PersonId,
                        Status = BookOrderStatus.Queued
                    });
                }
            }
            else if (command.Status == BookOrderStatus.Sold)
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
}
