namespace AS.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDateOfCreatedInPorductModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "CreatedAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "CreatedAt");
        }
    }
}
