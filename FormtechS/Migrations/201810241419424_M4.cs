namespace FormtechS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Addresses", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Orders", "CommunityId", "dbo.Communities");
            DropForeignKey("dbo.Communities", "Id", "dbo.Cities");
            DropIndex("dbo.Addresses", new[] { "CityId" });
            DropIndex("dbo.Communities", new[] { "Id" });
            DropIndex("dbo.Orders", new[] { "CommunityId" });
            DropPrimaryKey("dbo.Communities");
            AddColumn("dbo.Addresses", "CommunityId", c => c.Int(nullable: false));
            AddColumn("dbo.Communities", "CityId", c => c.Int(nullable: false));
            AlterColumn("dbo.Communities", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Communities", "Id");
            CreateIndex("dbo.Addresses", "CommunityId");
            CreateIndex("dbo.Communities", "CityId");
            AddForeignKey("dbo.Addresses", "CommunityId", "dbo.Communities", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Communities", "CityId", "dbo.Cities", "Id", cascadeDelete: true);
            DropColumn("dbo.Addresses", "CityId");
            DropColumn("dbo.Orders", "CommunityId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "CommunityId", c => c.Int(nullable: false));
            AddColumn("dbo.Addresses", "CityId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Communities", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Addresses", "CommunityId", "dbo.Communities");
            DropIndex("dbo.Communities", new[] { "CityId" });
            DropIndex("dbo.Addresses", new[] { "CommunityId" });
            DropPrimaryKey("dbo.Communities");
            AlterColumn("dbo.Communities", "Id", c => c.Int(nullable: false));
            DropColumn("dbo.Communities", "CityId");
            DropColumn("dbo.Addresses", "CommunityId");
            AddPrimaryKey("dbo.Communities", "Id");
            CreateIndex("dbo.Orders", "CommunityId");
            CreateIndex("dbo.Communities", "Id");
            CreateIndex("dbo.Addresses", "CityId");
            AddForeignKey("dbo.Communities", "Id", "dbo.Cities", "Id");
            AddForeignKey("dbo.Orders", "CommunityId", "dbo.Communities", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Addresses", "CityId", "dbo.Cities", "Id", cascadeDelete: true);
        }
    }
}
