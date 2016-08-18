namespace Bookify.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedBookContentTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BookContent", "Book_Id", "dbo.Book");
            DropIndex("dbo.BookContent", new[] { "Book_Id" });
            DropTable("dbo.BookContent");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.BookContent",
                c => new
                    {
                        BookId = c.Int(nullable: false, identity: true),
                        CoverPath = c.String(),
                        EpubPath = c.String(),
                        Book_Id = c.Int(),
                    })
                .PrimaryKey(t => t.BookId);
            
            CreateIndex("dbo.BookContent", "Book_Id");
            AddForeignKey("dbo.BookContent", "Book_Id", "dbo.Book", "Id");
        }
    }
}
