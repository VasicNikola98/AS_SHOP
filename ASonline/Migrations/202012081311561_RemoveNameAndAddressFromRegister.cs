namespace ASonline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveNameAndAddressFromRegister : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Name");
            DropColumn("dbo.AspNetUsers", "Address");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Address", c => c.String());
            AddColumn("dbo.AspNetUsers", "Name", c => c.String());
        }
    }
}
