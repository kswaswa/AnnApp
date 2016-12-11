namespace AnnApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ann9 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Anns", "Title", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Anns", "Content", c => c.String(nullable: false, maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Anns", "Content", c => c.String(nullable: false));
            AlterColumn("dbo.Anns", "Title", c => c.String(nullable: false));
        }
    }
}
