namespace AnnApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ann7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Ann_ID", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Ann_ID");
            AddForeignKey("dbo.AspNetUsers", "Ann_ID", "dbo.Anns", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Ann_ID", "dbo.Anns");
            DropIndex("dbo.AspNetUsers", new[] { "Ann_ID" });
            DropColumn("dbo.AspNetUsers", "Ann_ID");
        }
    }
}
