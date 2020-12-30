namespace AS.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReviewConfirmed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviews", "isConfirmed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reviews", "isConfirmed");
        }
    }
}
