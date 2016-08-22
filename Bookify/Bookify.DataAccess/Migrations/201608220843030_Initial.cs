namespace Bookify.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Address",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PersonId = c.Int(nullable: false),
                        Country = c.String(),
                        Street = c.String(),
                        ZipCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Person", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.PersonId);
            
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PublisherId = c.Int(),
                        Firstname = c.String(),
                        Lastname = c.String(),
                        Alias = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Publisher", t => t.PublisherId)
                .Index(t => t.PublisherId);
            
            CreateTable(
                "dbo.Publisher",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Trusted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Book",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AuthorId = c.Int(nullable: false),
                        PublisherId = c.Int(nullable: false),
                        Title = c.String(nullable: false),
                        Summary = c.String(),
                        ISBN = c.String(nullable: false),
                        Language = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PublishYear = c.Int(nullable: false),
                        PageCount = c.Int(),
                        CopiesAvailable = c.Int(),
                        ViewCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Author", t => t.AuthorId, cascadeDelete: true)
                .ForeignKey("dbo.Publisher", t => t.PublisherId, cascadeDelete: true)
                .Index(t => t.AuthorId)
                .Index(t => t.PublisherId);
            
            CreateTable(
                "dbo.Author",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BookFeedback",
                c => new
                    {
                        BookId = c.Int(nullable: false),
                        PersonId = c.Int(nullable: false),
                        Rating = c.Int(nullable: false),
                        Text = c.String(),
                    })
                .PrimaryKey(t => new { t.BookId, t.PersonId })
                .ForeignKey("dbo.Book", t => t.BookId, cascadeDelete: true)
                .Index(t => t.BookId);
            
            CreateTable(
                "dbo.Genre",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BookHistory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookId = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Attribute = c.String(),
                        PreviousValue = c.String(),
                        NewValue = c.String(),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Book", t => t.BookId, cascadeDelete: true)
                .Index(t => t.BookId);
            
            CreateTable(
                "dbo.BookOrder",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookId = c.Int(nullable: false),
                        PersonId = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        ReturnDatetime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Book", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Person", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.PersonId);
            
            CreateTable(
                "dbo.PersonRole",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PersonId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Person", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.PersonId);
            
            CreateTable(
                "dbo.Subscription",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PersonId = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Expires = c.DateTime(nullable: false),
                        Paid = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Person", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.PersonId);
            
            CreateTable(
                "dbo.BookGenre",
                c => new
                    {
                        BookId = c.Int(nullable: false),
                        GenreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BookId, t.GenreId })
                .ForeignKey("dbo.Book", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Genre", t => t.GenreId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.GenreId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subscription", "PersonId", "dbo.Person");
            DropForeignKey("dbo.PersonRole", "PersonId", "dbo.Person");
            DropForeignKey("dbo.Person", "PublisherId", "dbo.Publisher");
            DropForeignKey("dbo.Book", "PublisherId", "dbo.Publisher");
            DropForeignKey("dbo.BookOrder", "PersonId", "dbo.Person");
            DropForeignKey("dbo.BookOrder", "BookId", "dbo.Book");
            DropForeignKey("dbo.BookHistory", "BookId", "dbo.Book");
            DropForeignKey("dbo.BookGenre", "GenreId", "dbo.Genre");
            DropForeignKey("dbo.BookGenre", "BookId", "dbo.Book");
            DropForeignKey("dbo.BookFeedback", "BookId", "dbo.Book");
            DropForeignKey("dbo.Book", "AuthorId", "dbo.Author");
            DropForeignKey("dbo.Address", "PersonId", "dbo.Person");
            DropIndex("dbo.BookGenre", new[] { "GenreId" });
            DropIndex("dbo.BookGenre", new[] { "BookId" });
            DropIndex("dbo.Subscription", new[] { "PersonId" });
            DropIndex("dbo.PersonRole", new[] { "PersonId" });
            DropIndex("dbo.BookOrder", new[] { "PersonId" });
            DropIndex("dbo.BookOrder", new[] { "BookId" });
            DropIndex("dbo.BookHistory", new[] { "BookId" });
            DropIndex("dbo.BookFeedback", new[] { "BookId" });
            DropIndex("dbo.Book", new[] { "PublisherId" });
            DropIndex("dbo.Book", new[] { "AuthorId" });
            DropIndex("dbo.Person", new[] { "PublisherId" });
            DropIndex("dbo.Address", new[] { "PersonId" });
            DropTable("dbo.BookGenre");
            DropTable("dbo.Subscription");
            DropTable("dbo.PersonRole");
            DropTable("dbo.BookOrder");
            DropTable("dbo.BookHistory");
            DropTable("dbo.Genre");
            DropTable("dbo.BookFeedback");
            DropTable("dbo.Author");
            DropTable("dbo.Book");
            DropTable("dbo.Publisher");
            DropTable("dbo.Person");
            DropTable("dbo.Address");
        }
    }
}
