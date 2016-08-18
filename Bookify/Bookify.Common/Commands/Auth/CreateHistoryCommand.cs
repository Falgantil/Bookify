using System;
using Bookify.Common.Enums;

namespace Bookify.Common.Commands.Auth
{
    public class CreateHistoryCommand
    {
        public int BookId { get; set; }
        public BookHistoryType Type { get; set; }
        public DateTime Created { get; set; }
    }
}