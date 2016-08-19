using Bookify.Common.Models;

namespace Bookify.App.Core.Helpers
{
    public static class NameHelper
    {
        public static string GetFullName(this PersonDto account)
        {
            return $"{account.FirstName} {account.LastName}";
        }
    }
}
