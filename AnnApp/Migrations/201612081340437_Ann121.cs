namespace AnnApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ann121 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Anns", "Comments_ID", "dbo.Comments");
            DropIndex("dbo.Anns", new[] { "Comments_ID" });
            AddColumn("dbo.Comments", "comment", c => c.String());
            AddColumn("dbo.Comments", "AnnID", c => c.Int(nullable: false));
            DropColumn("dbo.Anns", "Comments_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Anns", "Comments_ID", c => c.Int());
            DropColumn("dbo.Comments", "AnnID");
            DropColumn("dbo.Comments", "comment");
            CreateIndex("dbo.Anns", "Comments_ID");
            AddForeignKey("dbo.Anns", "Comments_ID", "dbo.Comments", "ID");
        }
    }
}
