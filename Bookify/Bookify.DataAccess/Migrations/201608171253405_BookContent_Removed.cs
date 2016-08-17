namespace Bookify.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BookContent_Removed : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BookContent", "BookId", "dbo.Book");
            DropIndex("dbo.BookContent", new[] { "BookId" });
            DropPrimaryKey("dbo.BookContent");
            AddColumn("dbo.BookContent", "CoverPath", c => c.String());
            AddColumn("dbo.BookContent", "EpubPath", c => c.String());
            AddColumn("dbo.BookContent", "Book_Id", c => c.Int());
            AlterColumn("dbo.BookContent", "BookId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.BookContent", "BookId");
            CreateIndex("dbo.BookContent", "Book_Id");
            AddForeignKey("dbo.BookContent", "Book_Id", "dbo.Book", "Id");
            DropColumn("dbo.BookContent", "Cover");
            DropColumn("dbo.BookContent", "Epub");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BookContent", "Epub", c => c.Binary());
            AddColumn("dbo.BookContent", "Cover", c => c.Binary());
            DropForeignKey("dbo.BookContent", "Book_Id", "dbo.Book");
            DropIndex("dbo.BookContent", new[] { "Book_Id" });
            DropPrimaryKey("dbo.BookContent");
            AlterColumn("dbo.BookContent", "BookId", c => c.Int(nullable: false));
            DropColumn("dbo.BookContent", "Book_Id");
            DropColumn("dbo.BookContent", "EpubPath");
            DropColumn("dbo.BookContent", "CoverPath");
            AddPrimaryKey("dbo.BookContent", "BookId");
            CreateIndex("dbo.BookContent", "BookId");
            AddForeignKey("dbo.BookContent", "BookId", "dbo.Book", "Id");
        }
    }
}
