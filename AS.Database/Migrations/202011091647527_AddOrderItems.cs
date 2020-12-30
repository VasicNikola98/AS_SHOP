namespace AS.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrderItems : DbMigration
    {
        public override void Up()
        {
            CreateTable(
               "dbo.OrderItems",
               c => new
               {
                   Id = c.Int(nullable: false, identity: true),
                   OrderId = c.Int(nullable: false),
                   Quantity = c.Int(nullable: false),
                   ProductId = c.Int(nullable: false),
               })
               .PrimaryKey(t => t.Id);

            CreateIndex("dbo.OrderItems", "ProductId");
            CreateIndex("dbo.OrderItems", "OrderId");
            AddForeignKey("dbo.OrderItems", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.OrderItems", "OrderId", "dbo.Orders", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
        }
    }
}
