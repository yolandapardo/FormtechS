namespace FormtechS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M6 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Communities", "Name", c => c.String(nullable: false, maxLength: 80));
            AlterColumn("dbo.Cities", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Counties", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.States", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Orders", "ContactPerson", c => c.String(maxLength: 80));
            AlterColumn("dbo.Orders", "Phone", c => c.String(maxLength: 15));
            AlterColumn("dbo.Orders", "JobNumber", c => c.String(nullable: false, maxLength: 8));
            AlterColumn("dbo.Orders", "ReferenceNumber", c => c.String(maxLength: 8));
            AlterColumn("dbo.Orders", "Panel", c => c.String(maxLength: 10));
            AlterColumn("dbo.Orders", "FloodZone", c => c.String(maxLength: 3));
            AlterColumn("dbo.Clients", "Name", c => c.String(nullable: false, maxLength: 80));
            AlterColumn("dbo.Clients", "Phone", c => c.String(maxLength: 15));
            AlterColumn("dbo.Clients", "Email", c => c.String(maxLength: 50));
            AlterColumn("dbo.Companies", "Name", c => c.String(nullable: false, maxLength: 80));
            AlterColumn("dbo.Companies", "Phone", c => c.String(maxLength: 15));
            AlterColumn("dbo.Companies", "Email", c => c.String(maxLength: 50));
            AlterColumn("dbo.Companies", "Address", c => c.String(maxLength: 80));
            AlterColumn("dbo.TypeOfSurveys", "Type", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("dbo.Descriptions", new[] { "Lot", "Block", "Plat", "Page" }, unique: true, name: "IX_LotAndBlockAndPlatAndPage");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Descriptions", "IX_LotAndBlockAndPlatAndPage");
            AlterColumn("dbo.TypeOfSurveys", "Type", c => c.String(nullable: false));
            AlterColumn("dbo.Companies", "Address", c => c.String());
            AlterColumn("dbo.Companies", "Email", c => c.String());
            AlterColumn("dbo.Companies", "Phone", c => c.String());
            AlterColumn("dbo.Companies", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Clients", "Email", c => c.String());
            AlterColumn("dbo.Clients", "Phone", c => c.String());
            AlterColumn("dbo.Clients", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "FloodZone", c => c.String());
            AlterColumn("dbo.Orders", "Panel", c => c.String());
            AlterColumn("dbo.Orders", "ReferenceNumber", c => c.String());
            AlterColumn("dbo.Orders", "JobNumber", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "Phone", c => c.String());
            AlterColumn("dbo.Orders", "ContactPerson", c => c.String());
            AlterColumn("dbo.States", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Counties", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Cities", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Communities", "Name", c => c.String(nullable: false));
        }
    }
}
