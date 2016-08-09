using Bookify.Models;
using System.Data.Entity;
using System.Diagnostics;

namespace Bookify.DataAccess
{
    internal class BookifyContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }

        public BookifyContext() : base("DefaultConnection")
        {
            Configuration.LazyLoadingEnabled = false;
            Database.Log = s => Debug.WriteLine(s);
        }
    }
}
