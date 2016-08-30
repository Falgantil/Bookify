using Bookify.DataAccess.Configuration;
using Bookify.DataAccess.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics;

namespace Bookify.DataAccess
{
    public class BookifyContext : DbContext
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookFeedback> BookFeedback { get; set; }
        public DbSet<BookHistory> BookHistory { get; set; }
        public DbSet<BookOrder> BookOrders { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<PersonRole> PersonRoles { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }

        public BookifyContext() : base("DefaultConnection")
        {
            Database.Log = s => Debug.WriteLine(s);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new BookConfiguration());
            modelBuilder.Configurations.Add(new BookFeedbackConfiguration());

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BookifyContext, Migrations.Configuration>());
        }
    }
}
