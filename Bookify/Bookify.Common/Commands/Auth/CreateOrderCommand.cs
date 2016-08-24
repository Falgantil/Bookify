using System;
using Bookify.Common.Enums;

namespace Bookify.Common.Commands.Auth
{
    public class CreateOrderCommand
    {
        public int BookId { get; set; }
        public BookOrderStatus Status { get; set; }
        public int PersonId { get; set; }
    }
}