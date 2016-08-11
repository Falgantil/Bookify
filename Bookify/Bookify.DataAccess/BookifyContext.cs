﻿using Bookify.DataAccess.Configuration;
using Bookify.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics;

namespace Bookify.DataAccess
{
    internal class BookifyContext : DbContext
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookContent> BookContent { get; set; }
        public DbSet<BookHistory> BookHistory { get; set; }
        public DbSet<BookOrder> BookOrders { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<PersonRole> PersonRoles { get; set; }
        public DbSet<Publisher> Publishers { get; set; }

        public BookifyContext() : base("DefaultConnection")
        {
            Database.Log = s => Debug.WriteLine(s);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new BookConfiguration());
            modelBuilder.Configurations.Add(new BookContentConfiguration());
        }
    }
}