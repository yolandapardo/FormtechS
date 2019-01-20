namespace FormtechS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Communities", "CountyId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Communities", "CountyId", c => c.Int(nullable: false));
        }
    }
}
