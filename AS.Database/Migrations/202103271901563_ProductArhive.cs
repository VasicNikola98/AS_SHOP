namespace AS.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductArhive : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderItems", "ProductId", "dbo.Products");
            DropForeignKey("dbo.OrderItems", "OrderId", "dbo.Orders");
            DropIndex("dbo.OrderItems", new[] { "ProductId" });
            DropIndex("dbo.OrderItems", new[] { "OrderId" });
            RenameColumn(table: "dbo.OrderItems", name: "OrderId", newName: "Order_Id");
            AddColumn("dbo.OrderItems", "ProductName", c => c.String());
            AddColumn("dbo.OrderItems", "Price", c => c.Int(nullable: false));
            AddColumn("dbo.OrderItems", "ImageUrl", c => c.String());
            AddColumn("dbo.OrderItems", "PriceUnderline", c => c.Int(nullable: false));
            AlterColumn("dbo.OrderItems", "Order_Id", c => c.Int());
            CreateIndex("dbo.OrderItems", "Order_Id");
            AddForeignKey("dbo.OrderItems", "Order_Id", "dbo.Orders", "Id");
            DropColumn("dbo.OrderItems", "ProductId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderItems", "ProductId", c => c.Int(nullable: false));
            DropForeignKey("dbo.OrderItems", "Order_Id", "dbo.Orders");
            DropIndex("dbo.OrderItems", new[] { "Order_Id" });
            AlterColumn("dbo.OrderItems", "Order_Id", c => c.Int(nullable: false));
            DropColumn("dbo.OrderItems", "PriceUnderline");
            DropColumn("dbo.OrderItems", "ImageUrl");
            DropColumn("dbo.OrderItems", "Price");
            DropColumn("dbo.OrderItems", "ProductName");
            RenameColumn(table: "dbo.OrderItems", name: "Order_Id", newName: "OrderId");
            CreateIndex("dbo.OrderItems", "OrderId");
            CreateIndex("dbo.OrderItems", "ProductId");
            AddForeignKey("dbo.OrderItems", "OrderId", "dbo.Orders", "Id", cascadeDelete: true);
            AddForeignKey("dbo.OrderItems", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
        }
    }
}
