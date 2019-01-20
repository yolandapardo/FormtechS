namespace FormtechS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Communities", "CountyId", "dbo.Counties");
            DropIndex("dbo.Communities", new[] { "CountyId" });
            DropPrimaryKey("dbo.Communities");
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ZipCode = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        CountyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Counties", t => t.CountyId, cascadeDelete: true)
                .Index(t => t.CountyId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContactPerson = c.String(),
                        Precio = c.Single(nullable: false),
                        Phone = c.String(),
                        Fecha = c.DateTime(nullable: false),
                        JobNumber = c.String(nullable: false),
                        ReferenceNumber = c.String(),
                        Panel = c.String(),
                        Date = c.DateTime(nullable: false),
                        Suffix = c.String(maxLength: 1),
                        FloodZone = c.String(),
                        Elevation = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CommunityId = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                        ClientId = c.Int(nullable: false),
                        AddressId = c.Int(nullable: false),
                        TypeOfSurveyId = c.Int(nullable: false),
                        DescriptionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.AddressId, cascadeDelete: true)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.Communities", t => t.CommunityId, cascadeDelete: true)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .ForeignKey("dbo.Descriptions", t => t.DescriptionId, cascadeDelete: true)
                .ForeignKey("dbo.TypeOfSurveys", t => t.TypeOfSurveyId, cascadeDelete: true)
                .Index(t => t.CommunityId)
                .Index(t => t.CompanyId)
                .Index(t => t.ClientId)
                .Index(t => t.AddressId)
                .Index(t => t.TypeOfSurveyId)
                .Index(t => t.DescriptionId);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Phone = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Phone = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Descriptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Lot = c.String(),
                        Block = c.String(),
                        Plat = c.String(),
                        Page = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TypeOfSurveys",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.Communities", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.States", "Name", c => c.String(nullable: false));
            AddPrimaryKey("dbo.Communities", "Id");
            CreateIndex("dbo.Communities", "Id");
            AddForeignKey("dbo.Communities", "Id", "dbo.Cities", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cities", "CountyId", "dbo.Counties");
            DropForeignKey("dbo.Orders", "TypeOfSurveyId", "dbo.TypeOfSurveys");
            DropForeignKey("dbo.Orders", "DescriptionId", "dbo.Descriptions");
            DropForeignKey("dbo.Orders", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Orders", "CommunityId", "dbo.Communities");
            DropForeignKey("dbo.Orders", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.Orders", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Communities", "Id", "dbo.Cities");
            DropForeignKey("dbo.Addresses", "CityId", "dbo.Cities");
            DropIndex("dbo.Orders", new[] { "DescriptionId" });
            DropIndex("dbo.Orders", new[] { "TypeOfSurveyId" });
            DropIndex("dbo.Orders", new[] { "AddressId" });
            DropIndex("dbo.Orders", new[] { "ClientId" });
            DropIndex("dbo.Orders", new[] { "CompanyId" });
            DropIndex("dbo.Orders", new[] { "CommunityId" });
            DropIndex("dbo.Communities", new[] { "Id" });
            DropIndex("dbo.Cities", new[] { "CountyId" });
            DropIndex("dbo.Addresses", new[] { "CityId" });
            DropPrimaryKey("dbo.Communities");
            AlterColumn("dbo.States", "Name", c => c.String());
            AlterColumn("dbo.Communities", "Id", c => c.Int(nullable: false, identity: true));
            DropTable("dbo.TypeOfSurveys");
            DropTable("dbo.Descriptions");
            DropTable("dbo.Companies");
            DropTable("dbo.Clients");
            DropTable("dbo.Orders");
            DropTable("dbo.Cities");
            DropTable("dbo.Addresses");
            AddPrimaryKey("dbo.Communities", "Id");
            CreateIndex("dbo.Communities", "CountyId");
            AddForeignKey("dbo.Communities", "CountyId", "dbo.Counties", "Id", cascadeDelete: true);
        }
    }
}
