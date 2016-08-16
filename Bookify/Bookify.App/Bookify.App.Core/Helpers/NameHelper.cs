using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Bookify.Models;

namespace Bookify.App.Core.Helpers
{
    public static class NameHelper
    {
        public static string GetFullName(this Person account)
        {
            return $"{account.Firstname} {account.Lastname}";
        }
    }
}
