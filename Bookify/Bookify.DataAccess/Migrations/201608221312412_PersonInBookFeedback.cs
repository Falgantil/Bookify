namespace Bookify.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PersonInBookFeedback : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.BookFeedback", "PersonId");
            AddForeignKey("dbo.BookFeedback", "PersonId", "dbo.Person", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookFeedback", "PersonId", "dbo.Person");
            DropIndex("dbo.BookFeedback", new[] { "PersonId" });
        }
    }
}
