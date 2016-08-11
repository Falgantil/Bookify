using Bookify.Models;
using System.Data.Entity.ModelConfiguration;

namespace Bookify.DataAccess.Configuration
{
    internal class BookFeedbackConfiguration : EntityTypeConfiguration<BookFeedback>
    {
        internal BookFeedbackConfiguration()
        {
            HasKey(x => new { x.BookId, x.PersonId });
        }
    }
}
