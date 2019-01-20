namespace FormtechS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M7 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Addresses", "Name", c => c.String(nullable: false, maxLength: 80));
            CreateIndex("dbo.Clients", new[] { "Name", "Phone" }, unique: true, name: "IX_NameAndPhone");
            CreateIndex("dbo.Companies", "Name", unique: true);
            CreateIndex("dbo.TypeOfSurveys", "Type", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.TypeOfSurveys", new[] { "Type" });
            DropIndex("dbo.Companies", new[] { "Name" });
            DropIndex("dbo.Clients", "IX_NameAndPhone");
            AlterColumn("dbo.Addresses", "Name", c => c.String(nullable: false));
        }
    }
}
