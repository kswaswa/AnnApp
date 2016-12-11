namespace AnnApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ann3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Anns", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Anns", "Content", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Anns", "Content", c => c.String());
            AlterColumn("dbo.Anns", "Title", c => c.String());
        }
    }
}
