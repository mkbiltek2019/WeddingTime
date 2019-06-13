namespace AIT.WebUIComponent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "AutoLoginEnabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "AccountState", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "AccountState");
            DropColumn("dbo.AspNetUsers", "AutoLoginEnabled");
        }
    }
}
