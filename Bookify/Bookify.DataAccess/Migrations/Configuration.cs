using System.Collections.Generic;
using System.IO;
using Bookify.Common.Enums;

namespace Bookify.DataAccess.Migrations
{
    using Models;
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<BookifyContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(BookifyContext ctx)
        {
            var genres = new[]
            {
                new Genre {Id = 1, Name = "Biografi"},
                new Genre {Id = 2, Name = "Eventyr"},
                new Genre {Id = 3, Name = "Krimi"},
                new Genre {Id = 4, Name = "Science fiction"},
                new Genre {Id = 5, Name = "Børn"},
                new Genre {Id = 6, Name = "Humor"},
                new Genre {Id = 7, Name = "Spænding"}
            };
            ctx.Genres.AddOrUpdate(x => x.Id, genres);


            ctx.Authors.AddOrUpdate(x => x.Id,
                new Author { Id = 1, Name = "Jesper F. Jensen" },
                new Author { Id = 2, Name = "Maria Lang" });

            ctx.Publishers.AddOrUpdate(x => x.Id,
                new Publisher { Id = 1, Name = "DigTea", Trusted = true },
                new Publisher { Id = 2, Name = "Rosenkilde & Bahnhof", Trusted = false });
            ctx.SaveChanges();


            ctx.Persons.AddOrUpdate(x => x.Id,
            #region Person1
                new Person()
                {
                    Id = 1,
                    Alias = "xXxBjarkeBål69xXx",
                    Email = "admin@test.com",
                    Firstname = "Bjarka",
                    Lastname = "Søgaarde",
                    Password = EncryptSha512.GetPassword("test")
                },
            #endregion
            #region Person2
                new Person()
                {
                    Id = 2,
                    Alias = "Medlem guy",
                    Email = "medlem@test.com",
                    Firstname = "Med",
                    Lastname = "Lem",
                    Password = EncryptSha512.GetPassword("test")
                },
            #endregion
            #region Person3
                new Person()
                {
                    Id = 3,
                    Addresses =
                        new List<Address>()
                        {
                        },
                    Alias = "Medarbejder#56",
                    Email = "medarbejder@test.com",
                    Firstname = "Medarbejder",
                    Lastname = "#57",
                    Password = EncryptSha512.GetPassword("test")
                },
            #endregion
            #region Person4
                new Person()
                {
                    Id = 4,
                    Alias = "udgiver vognen",
                    Email = "test@test.com",
                    Firstname = "udgiver",
                    Lastname = "gyldendaal",
                    Password = EncryptSha512.GetPassword("test")
                }
                #endregion
            );
            ctx.SaveChanges();

            ctx.Addresses.AddOrUpdate(x => x.Id,
                            new Address() { Country = "Denmark", Street = "KarstenGade 10", ZipCode = 8293, PersonId = 1 },
                            new Address() { Country = "Denmark2", Street = "KarstenGade 13", ZipCode = 6548, PersonId = 2 },
                            new Address() { Country = "Denmark3", Street = "KarstenGade 16", ZipCode = 5548, PersonId = 3 },
                            new Address() { Country = "Denmark4", Street = "KarstenGade 17", ZipCode = 5664, PersonId = 4 }
                            );

            ctx.PersonRoles.AddOrUpdate(x => x.Id,
                new PersonRole() { Id = 1, PersonId = 1, Name = "Member" },
                new PersonRole() { Id = 2, PersonId = 1, Name = "Employee" },
                new PersonRole() { Id = 3, PersonId = 1, Name = "Publisher" },
                new PersonRole() { Id = 4, PersonId = 2, Name = "Member" },
                new PersonRole() { Id = 5, PersonId = 3, Name = "Employee" },
                new PersonRole() { Id = 6, PersonId = 4, Name = "Publisher" });
            ctx.SaveChanges();

            ctx.Books.AddOrUpdate(x => x.Id,
            #region Book1
                new Book
                {
                    Id = 1,
                    ISBN = "9788771744422",
                    AuthorId = 2,
                    PublisherId = 2,
                    Title = "Farlig at drikke",
                    Price = 250,
                    Summary = "Krimi fra et stockholmsk gymnasium, hvor skolekomediens primadonna og en kvindelig journalist bliver myrdet ved forårsballet. Christer Wijk mistænker en af skolens lærere for mordene",
                    Language = "Dansk",
                    PublishYear = 2015,
                    ViewCount = 0,
                    Genres = { genres[0] },
                    CopiesAvailable = 3,
                    History = new List<BookHistory>()
                    {
                        new BookHistory()
                        {
                            Type = BookHistoryType.Added,
                            Created = DateTime.Now
                        },
                        new BookHistory()
                        {
                            Type = BookHistoryType.Approved,
                            Created = DateTime.Now
                        },
                        new BookHistory()
                        {
                            Type = BookHistoryType.Changed,
                            Attribute = "Price",
                            PreviousValue = "100",
                            NewValue = "200",
                            Created = DateTime.Now
                        },new BookHistory()
                        {
                            Type = BookHistoryType.Changed,
                            Attribute = "Price",
                            PreviousValue = "200",
                            NewValue = "250",
                            Created = DateTime.Now
                        }
                    }
                },
            #endregion
            #region book2
                new Book
                {
                    Id = 2,
                    ISBN = "9788771744477",
                    AuthorId = 2,
                    PublisherId = 2,
                    Title = "Kun en skygge",
                    Price = 200,
                    Summary = "En ung kvinde vender hjem fra en udenlandsrejse og finder, i stedet for sin mand, en myrdet kvinde i sit badekar. Kriminalkommissær Christer Wijk får en vanskelig opgave med at trænge ind i det specielle akademikermiljø præget af misundelse og kærlighedsrelationer på kryds og tværs",
                    Language = "Dansk",
                    PublishYear = 2014,
                    ViewCount = 0,
                    Genres = { genres[2], genres[5], genres[6] },
                    CopiesAvailable = 20,
                    History = new List<BookHistory>()
                    {
                        new BookHistory()
                        {
                            Type = BookHistoryType.Added,
                            Created = DateTime.Now
                        },
                        new BookHistory()
                        {
                            Type = BookHistoryType.Approved,
                            Created = DateTime.Now
                        },
                        new BookHistory()
                        {
                            Type = BookHistoryType.Changed,
                            Attribute = "Title",
                            PreviousValue = "woah my first book!",
                            NewValue = "Kun en skygge",
                            Created = DateTime.Now
                        },new BookHistory()
                        {
                            Type = BookHistoryType.Changed,
                            Attribute = "Price",
                            PreviousValue = "3.50",
                            NewValue = "200",
                            Created = DateTime.Now
                        }
                    }
                },
            #endregion
            #region book3
                new Book
                {
                    Id = 3,
                    ISBN = "9788793183360",
                    AuthorId = 1,
                    PublisherId = 1,
                    Title = "Little Red Ridinghood",
                    Price = 150,
                    Summary = "Rødhætte er en lille artig pige. En dag sender mor hende ud til bedstemors hus. Men da hun møder hr. Ulv, glemmer hun, hvad mor har sagt",
                    Language = "Engelsk",
                    PublishYear = 2014,
                    ViewCount = 0,
                    Genres = { genres[1], genres[3], genres[4] },
                    CopiesAvailable = 4,
                    History = new List<BookHistory>()
                    {
                        new BookHistory()
                        {
                            Type = BookHistoryType.Added,
                            Created = DateTime.Now
                        },
                        new BookHistory()
                        {
                            Type = BookHistoryType.Approved,
                            Created = DateTime.Now
                        },
                        new BookHistory()
                        {
                            Type = BookHistoryType.Changed,
                            Attribute = "PublishYear",
                            PreviousValue = "0001",
                            NewValue = "2014",
                            Created = DateTime.Now
                        },new BookHistory()
                        {
                            Type = BookHistoryType.Deleted,
                            Created = DateTime.Now
                        }
                    }
                },
            #endregion
            #region book4
                new Book
                {
                    Id = 4,
                    ISBN = "9788771690194",
                    AuthorId = 1,
                    PublisherId = 1,
                    Title = "Thor and the ugly giant",
                    Price = 200,
                    Summary = "Der er kommet en jætte ind i Odins borg. Kan Thor få jætten jaget ud ved hjælp af list?",
                    Language = "Engelsk",
                    PublishYear = 2013,
                    ViewCount = 0,
                    Genres = { genres[2], genres[6], genres[3] },
                    CopiesAvailable = 4,
                    History = new List<BookHistory>()
                    {
                        new BookHistory()
                        {
                            Type = BookHistoryType.Added,
                            Created = DateTime.Now
                        },
                        new BookHistory()
                        {
                            Type = BookHistoryType.Approved,
                            Created = DateTime.Now
                        }
                    }
                });
            #endregion
            ctx.SaveChanges();


            ctx.BookOrders.AddOrUpdate(x => x.Id,
            #region BookOrders
                    new BookOrder()
                    {
                        Created = DateTime.Now,
                        PersonId = 1,
                        Status = BookOrderStatus.Sold,
                        BookId = 4
                    },
                    new BookOrder()
                    {
                        Created = DateTime.Now,
                        PersonId = 2,
                        Status = BookOrderStatus.Sold,
                        BookId = 4
                    },
                    new BookOrder()
                    {
                        Created = DateTime.Now,
                        PersonId = 3,
                        Status = BookOrderStatus.Sold,
                        BookId = 4
                    },
                    new BookOrder()
                    {
                        Created = DateTime.Now,
                        PersonId = 1,
                        Status = BookOrderStatus.Borrowed,
                        BookId = 4
                    },
                    new BookOrder()
                    {
                        Created = DateTime.Now,
                        PersonId = 1,
                        Status = BookOrderStatus.Queued,
                        BookId = 4
                    },
                    new BookOrder()
                    {
                        Created = DateTime.Now,
                        PersonId = 1,
                        Status = BookOrderStatus.Queued,
                        BookId = 4
                    },
                    new BookOrder()
                    {
                        Created = DateTime.Now,
                        PersonId = 1,
                        Status = BookOrderStatus.Queued,
                        BookId = 4
                    },
                    new BookOrder()
                    {
                        Created = DateTime.Now,
                        PersonId = 1,
                        Status = BookOrderStatus.Queued,
                        BookId = 4
                    },
                    new BookOrder()
                    {
                        Created = DateTime.Now,
                        PersonId = 2,
                        Status = BookOrderStatus.Queued,
                        BookId = 4
                    },
                    new BookOrder()
                    {
                        Created = DateTime.Now,
                        PersonId = 3,
                        Status = BookOrderStatus.Queued,
                        BookId = 4
                    },
                    new BookOrder()
                    {
                        Created = DateTime.Now,
                        PersonId = 4,
                        Status = BookOrderStatus.Borrowed,
                        BookId = 1
                    },
                    new BookOrder()
                    {
                        Created = DateTime.Now,
                        PersonId = 2,
                        Status = BookOrderStatus.Borrowed,
                        BookId = 1
                    }
                    #endregion
                );

            ctx.BookFeedback.AddOrUpdate(x => new { x.BookId, x.PersonId },
            #region BookFeedback
              new BookFeedback() { PersonId = 1, Rating = 5, Text = "woah oh my gawd diz book sooo for realz 5/7 - westside", BookId = 4 },
              new BookFeedback() { PersonId = 2, Rating = 0, Text = "Hvordan starter mand? Den her bog er mega dårlig\nDen har ikke engang en instruktions manual med...", BookId = 2 },
              new BookFeedback() { PersonId = 3, Rating = 3, Text = "Meh... den indeholder bogstaver... :|", BookId = 3 },
              new BookFeedback() { PersonId = 1, Rating = 5, Text = "I can't live without this", BookId = 2 },
              new BookFeedback() { PersonId = 4, Rating = 5, Text = "This is it, I see the light... from this book :O", BookId = 4 },
              new BookFeedback() { PersonId = 3, Rating = 2, Text = "Den indeholder masser af bogstaver!", BookId = 1 },
              new BookFeedback() { PersonId = 4, Rating = 1, Text = "Nah this aint what I'm lookin fo", BookId = 3},
              new BookFeedback() { PersonId = 2, Rating = 4, Text = "Ret god, fine farver", BookId = 3 },
              new BookFeedback() { PersonId = 2, Rating = 4, Text = "Min mad brændte på!", BookId = 1 },
              new BookFeedback() { PersonId = 4, Rating = 1, Text = "Kunne endelig læse bogen efter jeg snakkede med support i 139 timer >:(", BookId = 1 },
              new BookFeedback() { PersonId = 4, Rating = 0, Text = "Mah lord diz book make me sick!", BookId = 2 },
              new BookFeedback() { PersonId = 3, Rating = 0, Text = "Den levede desværre ikke op til mine forventninger", BookId = 2 }
              );
            #endregion
            ctx.SaveChanges();
        }
    }
}
