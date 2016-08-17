using System.IO;

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
                new Genre {Id = 4, Name = "Science fiction"}
            };
            ctx.Genres.AddOrUpdate(x => x.Id, genres);

            ctx.Authors.AddOrUpdate(x => x.Id,
                new Author { Id = 1, Name = "Jesper F. Jensen" },
                new Author { Id = 2, Name = "Maria Lang" });

            ctx.Publishers.AddOrUpdate(x => x.Id,
                new Publisher { Id = 1, Name = "DigTea", Trusted = true },
                new Publisher { Id = 2, Name = "Rosenkilde & Bahnhof", Trusted = false });

            ctx.SaveChanges();


            //Context.Books.Find(1).Genres.Add(Context.Genres.Find(3));
            //Context.Books.Find(2).Genres.Add(Context.Genres.Find(3));
            //Context.Books.Find(3).Genres.Add(Context.Genres.Find(2));
            //Context.Books.Find(4).Genres.Add(Context.Genres.Find(2));

            #region Book1
            ctx.Books.AddOrUpdate(x => x.Id,
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
                    Genres = { genres[0] }
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
                    Genres = { genres[2] }
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
                    Genres = { genres[1], genres[3] }
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
                    ViewCount = 0
                });
#endregion

            ctx.SaveChanges();
        }
    }
}
