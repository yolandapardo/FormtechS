namespace FormtechS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M8 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Addresses", new[] { "Name", "ZipCode" }, unique: true, name: "IX_NameAndZipCode");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Addresses", "IX_NameAndZipCode");
        }
    }
}
