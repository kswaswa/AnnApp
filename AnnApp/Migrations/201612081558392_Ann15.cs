namespace AnnApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ann15 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Vieweds",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Ann_ID = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Anns", t => t.Ann_ID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Ann_ID)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vieweds", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Vieweds", "Ann_ID", "dbo.Anns");
            DropIndex("dbo.Vieweds", new[] { "User_Id" });
            DropIndex("dbo.Vieweds", new[] { "Ann_ID" });
            DropTable("dbo.Vieweds");
        }
    }
}
