namespace CSharpTestMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
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
                        phoneNumber = c.Int(nullable: false),
                        streetName = c.String(),
                        city = c.String(),
                        province = c.String(),
                        postalCode = c.String(),
                        country = c.String(),
                        notes = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Users", "password", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "password");
            DropTable("dbo.Contacts");
        }
    }
}
