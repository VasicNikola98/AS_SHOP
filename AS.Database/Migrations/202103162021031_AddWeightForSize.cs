namespace AS.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWeightForSize : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductStocks", "DefaultWeight", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductStocks", "DefaultWeight");
        }
    }
}
