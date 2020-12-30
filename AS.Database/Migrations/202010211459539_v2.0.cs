namespace AS.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v20 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Description", c => c.String(maxLength: 500));
            DropColumn("dbo.Products", "Descripiton");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Descripiton", c => c.String(maxLength: 500));
            DropColumn("dbo.Products", "Description");
        }
    }
}
