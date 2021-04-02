namespace AS.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddArhivatePropertyInOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "IsArhivated", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "IsArhivated");
        }
    }
}
