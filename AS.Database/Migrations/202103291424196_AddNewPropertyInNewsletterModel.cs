namespace AS.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewPropertyInNewsletterModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Newsletters", "SubscribedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Newsletters", "IsVerified", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Newsletters", "IsVerified");
            DropColumn("dbo.Newsletters", "SubscribedAt");
        }
    }
}
