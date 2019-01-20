namespace FormtechS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M5 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Descriptions", "Lot", c => c.String(nullable: false, maxLength: 5));
            AlterColumn("dbo.Descriptions", "Block", c => c.String(maxLength: 5));
            AlterColumn("dbo.Descriptions", "Plat", c => c.String(maxLength: 5));
            AlterColumn("dbo.Descriptions", "Page", c => c.String(maxLength: 5));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Descriptions", "Page", c => c.String());
            AlterColumn("dbo.Descriptions", "Plat", c => c.String());
            AlterColumn("dbo.Descriptions", "Block", c => c.String());
            AlterColumn("dbo.Descriptions", "Lot", c => c.String());
        }
    }
}
