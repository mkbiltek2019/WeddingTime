namespace AIT.WebUIComponent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Groups",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    OrderNo = c.Int(),
                    UserId = c.String(),
                    Name = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.InnerGroups",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    GroupId = c.Int(nullable: false),
                    GroupKey = c.Guid(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .Index(t => t.GroupId);

            CreateTable(
                "dbo.Persons",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UniqueKey = c.Guid(),
                    GroupId = c.Int(nullable: false),
                    OrderNo = c.Int(),
                    Name = c.String(),
                    Surname = c.String(),
                    Phone = c.String(),
                    Email = c.String(),
                    Address = c.String(),
                    Status = c.Int(nullable: false),
                    Genre = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .Index(t => t.GroupId);

            CreateTable(
                "dbo.InnerGroupMembers",
                c => new
                {
                    PersonId = c.Int(nullable: false),
                    InnerGroupKey = c.Guid(nullable: false),
                })
                .PrimaryKey(t => t.PersonId)
                .ForeignKey("dbo.Persons", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.PersonId);

            CreateTable(
                    "dbo.BallroomItems",
                    c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BallroomId = c.Int(nullable: false),
                        PositionX = c.Int(nullable: false),
                        PositionY = c.Int(nullable: false),
                        Rotation = c.Int(nullable: false),
                        ItemType = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ballroom", t => t.BallroomId, cascadeDelete: true)
                .Index(t => t.BallroomId);

            CreateTable(
                    "dbo.Seats",
                    c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TableId = c.Int(nullable: false),
                        PersonId = c.Int(),
                        Hidden = c.Boolean(nullable: false),
                        TakenBy = c.Int(),
                        Location = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BallroomItems", t => t.TableId, cascadeDelete: true)
                .Index(t => t.TableId);

            CreateTable(
                    "dbo.Ballroom",
                    c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        Width = c.Int(nullable: false),
                        Height = c.Int(nullable: false),
                        IsExpanded = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                    "dbo.Tasks",
                    c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        Title = c.String(),
                        Description = c.String(),
                        ReminderDate = c.DateTime(),
                        State = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                    "dbo.TaskCards",
                    c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TaskId = c.Int(nullable: false),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tasks", t => t.TaskId, cascadeDelete: true)
                .Index(t => t.TaskId);

            CreateTable(
                    "dbo.TaskCardItems",
                    c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CardId = c.Int(nullable: false),
                        Value = c.String(),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TaskCards", t => t.CardId, cascadeDelete: true)
                .Index(t => t.CardId);

            CreateTable(
                    "dbo.webpages_OAuthMembership",
                    c => new
                    {
                        UserId = c.Int(nullable: false),
                        Provider = c.String(),
                        ProviderUserId = c.String(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                    "dbo.Users",
                    c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Email = c.String(),
                        UserKey = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                    "dbo.Budget",
                    c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                    "dbo.Expenses",
                    c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderNo = c.Int(),
                        Quantity = c.Int(),
                        UserId = c.String(),
                        Description = c.String(),
                        UnitPrice = c.Decimal(precision: 18, scale: 2),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                    "dbo.Undo",
                    c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        SerializedData = c.String(),
                        UniqueKey = c.Guid(nullable: false),
                        TypeKey = c.Guid(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Persons", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.InnerGroupMembers", "PersonId", "dbo.Persons");
            DropForeignKey("dbo.InnerGroups", "GroupId", "dbo.Groups");
            DropIndex("dbo.InnerGroupMembers", new[] { "PersonId" });
            DropIndex("dbo.Persons", new[] { "GroupId" });
            DropIndex("dbo.InnerGroups", new[] { "GroupId" });
            DropTable("dbo.InnerGroupMembers");
            DropTable("dbo.Persons");
            DropTable("dbo.InnerGroups");
            DropTable("dbo.Groups");

            DropForeignKey("dbo.BallroomItems", "BallroomId", "dbo.Ballroom");
            DropForeignKey("dbo.Seats", "TableId", "dbo.BallroomItems");
            DropIndex("dbo.Seats", new[] { "TableId" });
            DropIndex("dbo.BallroomItems", new[] { "BallroomId" });
            DropTable("dbo.Ballroom");
            DropTable("dbo.Seats");
            DropTable("dbo.BallroomItems");

            DropForeignKey("dbo.TaskCards", "TaskId", "dbo.Tasks");
            DropForeignKey("dbo.TaskCardItems", "CardId", "dbo.TaskCards");
            DropIndex("dbo.TaskCardItems", new[] { "CardId" });
            DropIndex("dbo.TaskCards", new[] { "TaskId" });
            DropTable("dbo.TaskCardItems");
            DropTable("dbo.TaskCards");
            DropTable("dbo.Tasks");

            DropForeignKey("dbo.webpages_OAuthMembership", "UserId", "dbo.Users");
            DropIndex("dbo.webpages_OAuthMembership", new[] { "UserId" });
            DropTable("dbo.Users");
            DropTable("dbo.webpages_OAuthMembership");

            DropTable("dbo.Expenses");
            DropTable("dbo.Budget");

            DropTable("dbo.Undo");
        }
    }
}
