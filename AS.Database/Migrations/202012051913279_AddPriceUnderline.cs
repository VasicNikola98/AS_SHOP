namespace AS.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPriceUnderline : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "PriceUnderline", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "PriceUnderline");
        }
    }
}
