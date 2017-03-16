namespace Platform.DataAccess.Resources.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AddressLine = c.String(),
                        City = c.String(),
                        Country = c.String(),
                        Zip = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Title = c.String(),
                        IsCompany = c.Boolean(nullable: false),
                        ContactType = c.String(),
                        Email = c.String(),
                        VatNumber = c.String(),
                        PhoneNumber = c.String(),
                        Address_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.Address_Id)
                .Index(t => t.Address_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contacts", "Address_Id", "dbo.Addresses");
            DropIndex("dbo.Contacts", new[] { "Address_Id" });
            DropTable("dbo.Contacts");
            DropTable("dbo.Addresses");
        }
    }
}
