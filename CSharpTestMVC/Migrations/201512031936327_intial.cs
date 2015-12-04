namespace CSharpTestMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        firstName = c.String(),
                        lastName = c.String(),
                        phoneNumber = c.String(),
                        streetName = c.String(),
                        city = c.String(),
                        province = c.String(),
                        postalCode = c.String(),
                        country = c.String(),
                        notes = c.String(),
                        contact_connection_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.contact_connection_ID)
                .Index(t => t.contact_connection_ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        userName = c.String(),
                        firstName = c.String(),
                        lastName = c.String(),
                        password = c.String(),
                        isAdmin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contacts", "contact_connection_ID", "dbo.Users");
            DropIndex("dbo.Contacts", new[] { "contact_connection_ID" });
            DropTable("dbo.Users");
            DropTable("dbo.Contacts");
        }
    }
}
