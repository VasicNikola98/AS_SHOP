namespace AS.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v14 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ImageUrl", c => c.String());
            AlterColumn("dbo.Products", "Category_Id", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Category_Id", c => c.Int());
            DropColumn("dbo.Products", "ImageUrl");
        }
    }
}
