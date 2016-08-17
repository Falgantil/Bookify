using Bookify.Models;
using System.Data.Entity.ModelConfiguration;

using Bookify.DataAccess.Models;

namespace Bookify.DataAccess.Configuration
{
    internal class BookContentConfiguration : EntityTypeConfiguration<BookContent>
    {
        internal BookContentConfiguration()
        {
            HasKey(x => x.BookId);
        }
    }
}
