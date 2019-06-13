namespace AIT.WebUIComponent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreationAndLastLoggingDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "CreationDate", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "LastLoginDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "LastLoginDate");
            DropColumn("dbo.AspNetUsers", "CreationDate");
        }
    }
}
