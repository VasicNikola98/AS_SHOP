namespace AS.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteArhivProduct : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ArhivedOrders", "OrderDetail_Id", "dbo.OrderDetails");
            DropForeignKey("dbo.OrderItems", "ArhivedOrder_Id", "dbo.ArhivedOrders");
            DropIndex("dbo.ArhivedOrders", new[] { "OrderDetail_Id" });
            DropIndex("dbo.OrderItems", new[] { "ArhivedOrder_Id" });
            DropColumn("dbo.OrderItems", "ArhivedOrder_Id");
            DropTable("dbo.ArhivedOrders");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ArhivedOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderedAt = c.DateTime(nullable: false),
                        Status = c.String(),
                        TotalAmount = c.Double(nullable: false),
                        OrderDetail_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.OrderItems", "ArhivedOrder_Id", c => c.Int());
            CreateIndex("dbo.OrderItems", "ArhivedOrder_Id");
            CreateIndex("dbo.ArhivedOrders", "OrderDetail_Id");
            AddForeignKey("dbo.OrderItems", "ArhivedOrder_Id", "dbo.ArhivedOrders", "Id");
            AddForeignKey("dbo.ArhivedOrders", "OrderDetail_Id", "dbo.OrderDetails", "Id");
        }
    }
}
