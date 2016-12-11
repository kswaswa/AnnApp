namespace AnnApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ann6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Anns", "Comments_ID", c => c.Int());
            CreateIndex("dbo.Anns", "Comments_ID");
            AddForeignKey("dbo.Anns", "Comments_ID", "dbo.Comments", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Anns", "Comments_ID", "dbo.Comments");
            DropIndex("dbo.Anns", new[] { "Comments_ID" });
            DropColumn("dbo.Anns", "Comments_ID");
            DropTable("dbo.Comments");
        }
    }
}
