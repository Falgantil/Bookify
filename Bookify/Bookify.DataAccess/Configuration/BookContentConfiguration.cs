using Bookify.DataAccess.Models;
using System.Data.Entity.ModelConfiguration;

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
