namespace AS.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDoubleTypeOfTotalAmount : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "TotalAmount", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "TotalAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
