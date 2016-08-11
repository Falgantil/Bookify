﻿using Bookify.Models;
using System.Data.Entity.ModelConfiguration;

namespace Bookify.DataAccess.Configuration
{
    internal class BookConfiguration : EntityTypeConfiguration<Book>
    {
        internal BookConfiguration()
        {
            HasRequired<BookContent>(b => b.Content)
                .WithRequiredPrincipal(x => x.Book);

            HasMany<Genre>(b => b.Genres)
                .WithMany(g => g.Books)
                .Map(gb =>
                {
                    gb.MapLeftKey("BookId");
                    gb.MapRightKey("GenreId");
                    gb.ToTable("BookGenre");
                });
        }
    }
}
