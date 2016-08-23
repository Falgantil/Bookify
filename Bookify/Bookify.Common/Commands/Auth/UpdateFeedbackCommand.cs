using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Common.Commands.Auth
{
    public class UpdateFeedbackCommand
    {
        public string Text { get; set; }
        public int? Rating { get; set; }
    }
}
