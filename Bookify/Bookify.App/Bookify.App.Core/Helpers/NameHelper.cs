using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Bookify.App.Core.Models;

namespace Bookify.App.Core.Helpers
{
    public static class NameHelper
    {
        public static string GetFullName(this AccountModel account)
        {
            return $"{account.FirstName} {account.LastName}";
        }
    }
}
