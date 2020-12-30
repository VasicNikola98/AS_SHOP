namespace AS.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRequiredFieldInOrderDetailsClass : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OrderDetails", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.OrderDetails", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.OrderDetails", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.OrderDetails", "Nummber", c => c.String(nullable: false));
            AlterColumn("dbo.OrderDetails", "City", c => c.String(nullable: false));
            AlterColumn("dbo.OrderDetails", "PostCode", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OrderDetails", "PostCode", c => c.String());
            AlterColumn("dbo.OrderDetails", "City", c => c.String());
            AlterColumn("dbo.OrderDetails", "Nummber", c => c.String());
            AlterColumn("dbo.OrderDetails", "Address", c => c.String());
            AlterColumn("dbo.OrderDetails", "LastName", c => c.String());
            AlterColumn("dbo.OrderDetails", "FirstName", c => c.String());
        }
    }
}
