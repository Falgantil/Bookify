using Bookify.Common.Models;

namespace Bookify.App.Core.Helpers
{
    /// <summary>
    /// The NameHelper extension class
    /// </summary>
    public static class NameHelper
    {
        /// <summary>
        /// Gets the full name.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <returns></returns>
        public static string GetFullName(this PersonDto account)
        {
            return $"{account.FirstName} {account.LastName}";
        }
    }
}
