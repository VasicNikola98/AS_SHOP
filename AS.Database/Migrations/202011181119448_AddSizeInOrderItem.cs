namespace AS.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSizeInOrderItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderItems", "Size", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderItems", "Size");
        }
    }
}
