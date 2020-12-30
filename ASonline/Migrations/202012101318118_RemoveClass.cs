namespace ASonline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveClass : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProfileModels", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ProfileModels", new[] { "ApplicationUser_Id" });
            DropTable("dbo.BillingAddresses");
            DropTable("dbo.ProfileModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProfileModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BillingAddresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        Nummber = c.String(),
                        Country = c.String(),
                        City = c.String(),
                        PostCode = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.ProfileModels", "ApplicationUser_Id");
            AddForeignKey("dbo.ProfileModels", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
