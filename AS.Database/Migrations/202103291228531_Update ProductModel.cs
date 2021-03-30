namespace AS.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProductModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "Description", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Description", c => c.String(maxLength: 1000));
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
