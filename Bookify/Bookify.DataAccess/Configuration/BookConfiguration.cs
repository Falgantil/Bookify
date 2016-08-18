using Bookify.DataAccess.Models;
using System.Data.Entity.ModelConfiguration;

namespace Bookify.DataAccess.Configuration
{
    internal class BookConfiguration : EntityTypeConfiguration<Book>
    {
        internal BookConfiguration()
        {
            HasMany(b => b.Genres)
                .WithMany(g => g.Books)
                .Map(gb =>
                {
                    gb.MapLeftKey("BookId");
                    gb.MapRightKey("GenreId");
                    gb.ToTable("BookGenre");
                });

            Property(x => x.Title).IsRequired();
            Property(x => x.ISBN).IsRequired();
            Property(x => x.Language).IsRequired();
        }
    }
}
