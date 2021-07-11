namespace Dejtinghemsida.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Entries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        AuthorId = c.String(),
                        RecipientId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PendingFriendRequests",
                c => new
                    {
                        FriendId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.FriendId, t.UserId });
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Kön = c.String(),
                        Förnamn = c.String(nullable: false),
                        Efternamn = c.String(nullable: false),
                        Ålder = c.Int(nullable: false),
                        Sysselsättning = c.String(nullable: false),
                        UserPhoto = c.Binary(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        ApplicationUser_Id1 = c.String(maxLength: 128),
                        ApplicationUser_Id2 = c.String(maxLength: 128),
                        PendingFriendRequests_FriendId = c.String(maxLength: 128),
                        PendingFriendRequests_UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id1)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id2)
                .ForeignKey("dbo.PendingFriendRequests", t => new { t.PendingFriendRequests_FriendId, t.PendingFriendRequests_UserId })
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id1)
                .Index(t => t.ApplicationUser_Id2)
                .Index(t => new { t.PendingFriendRequests_FriendId, t.PendingFriendRequests_UserId });
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Interests",
                c => new
                    {
                        InterestID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.InterestID);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Friends",
                c => new
                    {
                        FriendId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.FriendId, t.UserId });
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.användares_Intressen",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 128),
                        InterestID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.id, t.InterestID })
                .ForeignKey("dbo.AspNetUsers", t => t.id, cascadeDelete: true)
                .ForeignKey("dbo.Interests", t => t.InterestID, cascadeDelete: true)
                .Index(t => t.id)
                .Index(t => t.InterestID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUsers", new[] { "PendingFriendRequests_FriendId", "PendingFriendRequests_UserId" }, "dbo.PendingFriendRequests");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "ApplicationUser_Id2", "dbo.AspNetUsers");
            DropForeignKey("dbo.användares_Intressen", "InterestID", "dbo.Interests");
            DropForeignKey("dbo.användares_Intressen", "id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "ApplicationUser_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.användares_Intressen", new[] { "InterestID" });
            DropIndex("dbo.användares_Intressen", new[] { "id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "PendingFriendRequests_FriendId", "PendingFriendRequests_UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "ApplicationUser_Id2" });
            DropIndex("dbo.AspNetUsers", new[] { "ApplicationUser_Id1" });
            DropIndex("dbo.AspNetUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropTable("dbo.användares_Intressen");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Friends");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.Interests");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.PendingFriendRequests");
            DropTable("dbo.Entries");
        }
    }
}
