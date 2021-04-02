namespace AS.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddArhivedOrdersEntity : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrderDetails", t => t.OrderDetail_Id)
                .Index(t => t.OrderDetail_Id);
            
            AddColumn("dbo.OrderItems", "ArhivedOrder_Id", c => c.Int());
            CreateIndex("dbo.OrderItems", "ArhivedOrder_Id");
            AddForeignKey("dbo.OrderItems", "ArhivedOrder_Id", "dbo.ArhivedOrders", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderItems", "ArhivedOrder_Id", "dbo.ArhivedOrders");
            DropForeignKey("dbo.ArhivedOrders", "OrderDetail_Id", "dbo.OrderDetails");
            DropIndex("dbo.OrderItems", new[] { "ArhivedOrder_Id" });
            DropIndex("dbo.ArhivedOrders", new[] { "OrderDetail_Id" });
            DropColumn("dbo.OrderItems", "ArhivedOrder_Id");
            DropTable("dbo.ArhivedOrders");
        }
    }
}
